import { Component } from 'react';
import axios from 'axios';
import ChallengeDTO from '../DTO/ChallengeDTO';
import RecipeDTO from '../DTO/RecipeDTO'
import '../styles/JuryScreen-style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faThumbsUp, faThumbsDown} from '@fortawesome/free-solid-svg-icons';

class JuryScreen extends Component {
    constructor(props) {
        super(props);
        this.state = {
            currentScreen: 'screen1',
            Challenges: [],
            selectedChallengeId: null,
            Recipes: [],
            ChallengeName: '',
            currentRecipeIndex: 0,
        };
        this.API_URL = "http://localhost:5221/";
    }


    switchScreen = async (direction) => {
        const { currentScreen, selectedChallengeId, currentRecipeIndex } = this.state;
        const screens = ['screen1', 'screen2', 'screen3'];
        const currentIndex = screens.indexOf(currentScreen);

        if (currentScreen === 'screen1' && direction === 'next') {
            await this.refreshChallengesRecipe(selectedChallengeId);
            this.setState({ currentScreen: 'screen2' });
        }

        if ((currentScreen === 'screen2' && direction === 'next') || (currentScreen === 'screen2' && direction === 'nextRecipe') ) {
            if (currentRecipeIndex < this.state.Recipes.length - 1) {
                this.setState({ currentRecipeIndex: currentRecipeIndex + 1 });
            } else {
                this.setState({ currentScreen: 'screen3' });
            }
        }

        if (currentScreen === 'screen3' && direction === 'next') {
            console.log("screen 3");
            window.location.reload(true);
        }

        if (direction === 'next' && currentIndex < screens.length - 1) {
            this.setState({ currentScreen: screens[currentIndex + 1] });
        } else if (direction === 'next' && currentIndex === screens.length - 1) {
            this.setState({ currentScreen: 'screen1' });
        }
    };


    async componentDidMount() {
        await this.refreshChallenges();
    }

    async refreshChallenges() {
        try {
            const response = await axios.get(this.API_URL + "api/Challenge");

            const challengeData = response.data.map(challenge => new ChallengeDTO(
                challenge.challengeId,
                challenge.challengeName,
                challenge.description,
                challenge.startDate,
                challenge.endDate,
            ));

            this.setState({ Challenges: challengeData });
        } catch (error) {
            console.error("Fout bij het ophalen van challenges:", error);
        }
    }

    handleChallengeChange = (event) => {
        this.setState({ selectedChallengeId: event.target.value });
    }

    async refreshChallengesRecipe(challengeId) {
        try {
            console.log("Fetching data for Challenge ID:", challengeId);
            const response = await axios.get(this.API_URL + `api/Challenge/${challengeId}`);
            console.log("API Response:", response.data);

            const recipes = response.data.recipes;

            const recipeData = recipes.map(recipe => {
                const firstImage = recipe.images.length > 0 ? recipe.images[0].imageUrl : null;

                return new RecipeDTO(
                    recipe.recipeId,
                    recipe.userId,
                    recipe.recipeName,
                    recipe.description,
                    firstImage,
                );
            });

            console.log("Recipe Data:", recipeData);

            this.setState({ ChallengeName: response.data.challengeName });
            this.setState({ Recipes: recipeData });
        } catch (error) {
            console.error("Fout bij het ophalen van recipes:", error);
        }
    }

    async addLike(recipeID) {
        try {
            await axios.post(this.API_URL + `api/Like/Recipe/${recipeID}/Like`, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });
        } catch (error) {
            alert('Network error: ' + error.message);
        }
    }

    render() {
        const { currentScreen, Recipes, ChallengeName, currentRecipeIndex } = this.state;

        return (
            <div className='JureScreen'>
                    {currentScreen === 'screen1' && (
                        <div className='screen-content'>
                            <h1>Welkom bij de Cooking Competition Tinder</h1>
                            <p>Kies hier de Challenge waar jij de recepten wilt beoordelen</p>
                            <form className='form'>
                                <select className='select'
                                    value={this.state.selectedChallengeId || ''}
                                    onChange={this.handleChallengeChange}
                                >
                                    <option value="" disabled>Kies een uitdaging</option>
                                    {this.state.Challenges.map(challenge => (
                                        <option key={challenge.challengeId} value={challenge.challengeId}>
                                            {challenge.challengeName}
                                        </option>
                                    ))}
                                </select>
                            </form>
                        </div>
                    )}
                    {currentScreen === 'screen2' && (
                        <div className='screen-content screen2'>
                            <p>
                                Klik op <FontAwesomeIcon icon={faThumbsUp} className='Icon-Like'/> als je het recept leuk vind 
                                of 
                                klik op <FontAwesomeIcon icon={faThumbsDown} className='Icon-Dislike'/> als je het recept niet leuk vind
                            </p>
                            <div className='challengeName'>
                                <p>{ChallengeName}</p>
                            </div>
                            {Recipes.length > 0 ? (
                                <div className='Recipe-Content'>
                                    <p>{`Recept ${currentRecipeIndex + 1} van ${Recipes.length}`}</p>
                                    <div className="ChallengesRecipeContent">
                                        {/* Toon hier de gegevens voor het huidige recept */}
                                        <div className="RecipeIMG">
                                            <img className="RecipeIMG" src={`src/assets/${Recipes[currentRecipeIndex].imageUrl.replace(/\\/g, '/')}`} alt={`Recipe ${Recipes[currentRecipeIndex].recipeName}`} />
                                        </div>
                                        <div>
                                            <p className="RecipeName">
                                                Receptnaam: {Recipes[currentRecipeIndex].recipeName}
                                            </p>
                                            <p className="RecipeDescription">
                                                Beschrijving: {Recipes[currentRecipeIndex].description}
                                            </p>
                                        </div>
                                        {/* Voeg hier eventuele andere receptinformatie toe */}
                                    </div>
                                    <div className="navigation-buttons-stem">
                                        <button
                                            className='stem like'
                                            onClick={() => {
                                                this.addLike(Recipes[currentRecipeIndex].recipeId);
                                                this.switchScreen('nextRecipe');
                                            }}
                                        >
                                            <FontAwesomeIcon icon={faThumbsUp} className='Icon-Like'/>
                                        </button>

                                        <button className='stem dislike' onClick={() => this.switchScreen('nextRecipe')}>
                                            <FontAwesomeIcon icon={faThumbsDown} className='Icon-Dislike'/>
                                        </button>
                                    </div>
                                </div>
                            ) : (
                                <div>
                                    <p>Geen challenge aangeduid</p>
                                    <p>of</p>
                                    <p>Geen recepten beschikbaar voor deze challenge</p>
                                </div>
                            )}
                        </div>
                    )}
                    {currentScreen === 'screen3' && (
                        <div className='screen-content'>
                            <h2>Bedankt voor het stemmen</h2>
                            <p>Klik op Ga verder om terug naar begin scherm te gaan</p>
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
        );
    }
}

export default JuryScreen;
