import { Component } from 'react';
import axios from 'axios';
import '../styles/PopUp-style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark, faCheck} from '@fortawesome/free-solid-svg-icons';
import { } from '@fortawesome/free-regular-svg-icons';

class ChallengePopup extends Component {
    constructor(props) {
        super(props);
        this.state = {
            currentScreen: 'screen1',
            User: {},
            selectedFiles: [],
            recipeID: 0,
        };
        this.API_URL = "http://localhost:5221/";

        this.user = {
            email: '',
        };

        this.recipe = {
            recipeName: '',
            recipeDescription: ''
        }
    }

    switchScreen = async (direction) => {
        const { currentScreen } = this.state;
        const screens = ['screen1', 'screen2', 'screen3', 'screen4'];
        const currentIndex = screens.indexOf(currentScreen);

        if (currentScreen === 'screen1' && direction === 'next') {
            const email = document.getElementsByName('email')[0].value;

            if (email.trim() !== '') {
                await this.GetUser(email);
            } else {
                alert('Please enter a valid email address before proceeding.');
                return;
            }
        }

        if (currentScreen === 'screen2' && direction === 'next') {
            const naam = document.getElementsByName('naam')[0].value;
            const beschrijving = document.getElementsByName('beschrijving')[0].value;

            if (naam.trim() === '' || beschrijving.trim() === '') {
                alert('Please fill in all fields before proceeding.');
                return;
            }

            this.recipe.recipeName = naam;
            this.recipe.recipeDescription = beschrijving;

            await this.addRecipe();
        }

        if (currentScreen === 'screen3' && direction === 'next') {
            if (this.state.selectedFiles.length >= 3) {
                for (let i = 0; i < this.state.selectedFiles.length; i++) {
                    await this.addImage(this.state.selectedFiles[i]);
                }
            } else {
                alert('Selecteer minimaal 3 & maximaal 5 bestanden.');
                return;
            }
        }

        if (currentScreen === 'screen4' && direction === 'next') {
            const { onClose } = this.props;
            if (onClose) {
                onClose();
            }

            window.location.reload(true);
        }

        if (direction === 'next' && currentIndex < screens.length - 1) {
            this.setState({ currentScreen: screens[currentIndex + 1] });
        }
    };

    async addUser() {
        try {
            const response = await axios.post(this.API_URL + 'api/User', 
            {
                email: this.user.email,
            },
            {
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.status === 200) {
                await this.GetUser(this.user.email);
            } else {
                alert('Failed to add user. Please try again.');
            }
        } catch (error) {
            alert('Network error: ' + error.message);
        }
    }

    async GetUser(email) {
        try {
            console.log("Fetching data for User Email:", email);
            const encodedEmail = encodeURIComponent(email);
            const response = await axios.get(this.API_URL + `api/User/${encodedEmail}`);
            console.log("API Response:", response.data);

            this.setState({ User: response.data });
        } catch (error) {
            console.log("User not found. Calling addUser.");
            this.user.email = email;
            await this.addUser();
        }
    }

    async addRecipe() {
        try {

            const emailUser = this.state.User
            const encodedEmail = encodeURIComponent(emailUser.email);

            // eslint-disable-next-line react/prop-types
            const { challengeId } = this.props;

            if (!challengeId) {
                alert('ChallengeId is missing.');
                return;
            }

            const response = await axios.post(
                this.API_URL + `api/Recipe/Challenge/${challengeId}/User/${encodedEmail}/Recipe`, 
                {
                    recipeName: this.recipe.recipeName,
                    recipeDescription: this.recipe.recipeDescription,
                },
                {
                    headers: {
                        'Content-Type': 'application/json',
                    },
                }
            );

            this.setState({ recipeID: response.data.recipeId });
        } catch (error) {
            console.error('Network error:', error);
            alert('Network error: ' + error.message);
        }
    }

    handleFileUpload = (event) => {
        const files = event.target.files;

        if (files.length >= 3 && files.length <= 5) {
            const filesArray = Array.from(files);

            this.setState({ selectedFiles: filesArray });
        } else {
            alert('Selecteer tussen 3 en 5 bestanden.');
        }
    };

    async addImage(file) {
        try {
            const recipeId = this.state.recipeID;

            if (!recipeId) {
                alert('RecipeId is missing.');
                return;
            }

            const formData = new FormData();
            formData.append('image', file);

            const response = await axios.post(
                this.API_URL + `api/recipe/${recipeId}/Image/upload`,
                formData,
                {
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'multipart/form-data',
                    },
                }
            );

            console.log('Image upload response:', response.data);
        } catch (error) {
            console.error('Network error:', error);
            alert('Network error: ' + error.message);
        }
    }

    render() {
        // eslint-disable-next-line react/prop-types, no-unused-vars
        const { onClose, challengeId } = this.props;
        const { currentScreen } = this.state;

        return (
            <div className="popup">
                <div className="popup-screen">
                    <div className="top">
                        <button onClick={onClose}>
                            <FontAwesomeIcon icon={faXmark} className='Icon'/>
                        </button>
                    </div>
                    {currentScreen === 'screen1' && (
                        <div className='popup-content'>
                            <h2 className='title'>Login of Registreer</h2>
                            <div className='input' >
                                <label className='label' htmlFor="email">Vul hier jouw E-mail in</label>
                                <input className='input-box' type="email" name='email'/>
                            </div>
                        </div>
                    )}
                    {currentScreen === 'screen2' && (
                        <div className='popup-content'>
                            <h2 className='title'>Voeg jouw recept toe</h2>
                            <p>
                                Tip : maak er iets leuk en creatief van, zo verhoog je kansen om te winnen!
                            </p>
                            <div className='input' >
                                <label className='label' htmlFor="naam">Naam recept</label>
                                <input className='input-box' type="text" name='naam'/>
                                <label className='label' htmlFor="beschrijving">Beschrijving recept</label>
                                <input className='input-box' type="text" name='beschrijving'/>
                            </div>
                        </div>
                    )}
                    {currentScreen === 'screen3' && (
                        <div className='popup-content'>
                            <h2 className='title'>voeg jouw beste foto's toe</h2>
                            <input type="file" id="file" name="file" multiple onChange={this.handleFileUpload}/>
                        </div>
                    )}
                    {currentScreen === 'screen4' && (
                        <div className='popup-content'>
                            <FontAwesomeIcon icon={faCheck} className='Check'/>
                            <h2 className='title'>Alright, je recept werd ingediend!</h2>
                            <p>
                                Onze kookcel zullen nu je recept reviewen. We laten je snel wat weten!
                            </p>
                        </div>
                    )}
                    <div className="navigation-buttons">
                        <button className='Action-btn-2' onClick={() => this.switchScreen('next')}>
                            <p className="btn-txt-2">
                                 Ga verder
                            </p>
                        </button>
                    </div>
                </div>
            </div>
        );
    }
}

export default ChallengePopup;

