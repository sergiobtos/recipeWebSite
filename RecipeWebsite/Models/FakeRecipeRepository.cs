using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeWebsite.Models
{
    public class FakeRecipeRepository
    {
        private List<Recipe> _recipeList;

        public FakeRecipeRepository()
        {
            _recipeList = new List<Recipe>()
                 {
                 new Recipe(){RecipeId= 1, RecipeName="Apple Butter Spice Cake",  Ingredients="Topping: 1 cup packed brown sugar, 1 teaspoon ground cinnamon,1/2 teaspoon ground nutmeg, 1/2 cup choppedpecans Cake: 2 cups all-purpose flour, 1 teaspoon baking powder, 1 teaspoon baking soda, 1/2 teaspoon salt, 1/2 cup butter, 1 cup white sugar.", RecipeInstructions= "\n1-Preheat oven to 350 degrees F (175 degrees C). Grease a 9x13-inch pan.\n2-Prepare the topping by mixing together the brown sugar, cinnamon, nutmeg, and chopped pecans."},
                 new Recipe(){RecipeId = 2 , RecipeName="Homemade Melt Dark Chocolate",  Ingredients="1/2 cup coconut oil, 1/2 cup cocoa powder, 3 tablespoons honey, 1/2 teaspoon vanilla extract.", RecipeInstructions= "\nGently melt coconut oil in a saucepan over medium-low heat. Stir cocoa powder, honey, and vanilla extract into melted oil until well blended. Pour mixture into a candy mold or pliable tray. Refrigerate until chilled, about 1 hour."}
                };

        }

        public Recipe Add(Recipe recipe)
        {
            recipe.RecipeId = _recipeList.Max(e => e.RecipeId) + 1;
            _recipeList.Add(recipe);
            return recipe;
        }

        public Review AddReview(int id, Review review)
        {
           Recipe recipe = _recipeList.FirstOrDefault(e => e.RecipeId == id);
            recipe.Reviews.Add(review);

            return review;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipeList;
        }

        public Recipe GetRecipe(int id)
        {
            return _recipeList.FirstOrDefault(e => e.RecipeId == id);
        }
    }
}
