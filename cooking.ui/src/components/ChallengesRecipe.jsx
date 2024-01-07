import { Component } from 'react';
import '../styles/ChallengesRecipe-style.css';
import PropTypes from 'prop-types';
import RecipeDTO from '../DTO/RecipeDTO';
import axios from 'axios';

import { } from '@fortawesome/free-solid-svg-icons';
import { } from '@fortawesome/free-regular-svg-icons';

class Recipe extends Component {

    constructor(props){
        super(props);
        this.state ={
            Recipes:[]
        };
        this.API_URL = "http://localhost:5221/";
    }

    static propTypes = {
        challengeId: PropTypes.number,
    }

    componentDidMount(){
        const { challengeId } = this.props;
        console.log("Challenge ID:", challengeId);
        this.refreshChallenges(challengeId);
    }

    componentDidUpdate(prevProps) {
        const { challengeId } = this.props;
        if (challengeId !== prevProps.challengeId) {
            this.refreshChallenges(challengeId);
        }
    }

    async refreshChallenges(challengeId) {
        try{
            console.log("Fetching data for Challenge ID:", challengeId);
            const response = await axios.get(this.API_URL + `api/Challenge/${challengeId}`);
            console.log("API Response:", response.data);

            const recipes = response.data.recipes.slice(0, 4);

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

            this.setState({ Recipes: recipeData });
        } catch (error) {
            console.error("Fout bij het ophalen van recipes:", error);
        }
    }

    render() {
        const { Recipes } = this.state;

        if (Recipes.length === 0) {
            return <p>Geen recepten beschikbaar voor deze uitdaging.</p>;
        }

        return (
            <>
                {Recipes.map((recipe, index) => (
                    <div className="ChallengesRecipeContent" key={recipe.recipeId || index}>
                        <div key={`RecipeIMG_${recipe.recipeId}`} className="RecipeIMG">
                            <img className="RecipeIMG" src={`src/assets/${recipe.imageUrl.replace(/\\/g, '/')}`} alt={`Recipe ${recipe.recipeName}`} />
                        </div>
                        <p key={`RecipeName_${recipe.recipeId}`} className="RecipeName">
                            {recipe.recipeName}
                        </p>
                    </div>
                ))}
            </>
        );
    }
}

export default Recipe;