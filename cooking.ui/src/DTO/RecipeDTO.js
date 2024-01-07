class RecipeDTO {
    constructor(recipeId, userId, recipeName, description, imageUrl) {
        this.recipeId = recipeId;
        this.userId = userId;
        this.recipeName = recipeName;
        this.description = description;
        this.imageUrl = imageUrl;
    }
}

export default RecipeDTO;