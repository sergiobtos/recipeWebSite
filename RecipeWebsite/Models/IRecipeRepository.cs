using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeWebsite.Models
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> Recipes { get; }
        Recipe GetRecipe(int id);
        IEnumerable<Recipe> GetAllRecipes();
        Recipe Add(Recipe recipe);
        void AddReview(Review review);
        Recipe Delete(int id);
        void SaveRecipe(Recipe recipe);

    }
}
