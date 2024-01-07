import { Component } from 'react'
import ChallengesRecipe from './ChallengesRecipe';
import ChallengeDTO from '../DTO/ChallengeDTO';
import '../styles/Challenges-style.css'
import axios from 'axios';
import PopUp from './PopUp';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { } from '@fortawesome/free-solid-svg-icons';
import { } from '@fortawesome/free-regular-svg-icons';

class Challenges extends Component {
    constructor(props) {
        super(props);
        this.state = {
            Challenges: [],
            isPopupOpen: false,
            selectedChallengeId: null,
        };
        this.API_URL = "http://localhost:5221/";
    }

    componentDidMount(){
        this.refreshChallenges();
    }

    async refreshChallenges() {
        try{
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
            console.error("Fout bij het ophalen van challenges:", error)
        }
    }

    // async refreshChallenges() {
    //     fetch(this.API_URL + "api/Challenge").then(response => response.json()).then(data => {
    //         const challengeData = data.map(challenge => new ChallengeDTO(
    //             challenge.challengeId,
    //             challenge.challengeName,
    //             challenge.description,
    //             challenge.startDate,
    //             challenge.endDate,
    //         ));

    //         this.setState({
    //             Challenges : challengeData
    //         });
    //     })
            
    // }

    openPopup = (challengeId) => {
        this.setState({
            isPopupOpen: true,
            selectedChallengeId: challengeId,
        });
    };

    closePopup = () => {
        this.setState({
            isPopupOpen: false,
            selectedChallengeId: null,
        });
    };

    render() {

        const monthName = ["januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december"];

        const ColorArr1 = ["#FF9A22", "#668B5C", "#5D78B1"];
        const ColorArr2 = ["#F6DCBD", "#DEECDA", "#D9E3F8"];
        const ColorArr3 = ["#F9F2EA", "#F1F7EF", "#EAEFF9"];

        const BeginTextTest = ["Wie maakt de", "Wie maakt de", "Wie maakt de"];

        const { Challenges, isPopupOpen, selectedChallengeId } = this.state;

        return (
            <>
                {Challenges.map((challenge, index) => (
                    <div className="Challenges" key={challenge.challengeId}>
                        <div className="ChallengeMain" style={{ backgroundColor: ColorArr2[index % 3] }}>
                            <button className='Action-btn-2' onClick={() => this.openPopup(challenge.challengeId)}>
                                <p className="btn-txt-2">doe mee!</p>
                            </button>
                            <div className="ChallengesText">
                                <p className="month">
                                    {monthName[new Date(challenge.startDate).getMonth()]}
                                    {/* {monthTest[index % 3]} */}
                                </p>
                                <p className='ChallengeTitle ' style={{ color: ColorArr1[index % 3] }}>
                                    <span className="BeginText">
                                        {BeginTextTest[index % 3]}
                                        <br/>
                                    </span>
                                    {challenge.challengeName}
                                </p>
                            </div>
                        </div>
                        <div className="ChallengeRecipe" style={{ backgroundColor: ColorArr3[index % 3] }}>
                            <div className="ChallengeRecipeMain">
                                <ChallengesRecipe key={challenge.challengeId} challengeId={challenge.challengeId} />
                            </div>
                        </div>
                    </div>
                ))}


                {isPopupOpen && (
                    <PopUp
                        onClose={this.closePopup}
                        challengeId={selectedChallengeId}
                    />
                )}
            </>
        )
    }
}

export default Challenges;
