using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RecipeWebsite.Models
{
    public class SQLRecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext context;

        public IQueryable<Recipe> Recipes => context.Recipes; 

        public SQLRecipeRepository(AppDbContext ctx)
        {
            context = ctx;
        }
        public Recipe Add(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
            return recipe;
        }

        public void AddReview( Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return context.Recipes.Include(rec => rec.Reviews);
        }

        public Recipe GetRecipe(int id)
        {
            return GetAllRecipes().FirstOrDefault(rec => rec.RecipeId == id);
        }

        public Recipe Delete(int id)
        {
            Recipe recipeEntry = context.Recipes
                .FirstOrDefault(rec => rec.RecipeId == id);
            if (recipeEntry != null)
            {
                context.Recipes.Remove(recipeEntry);
                context.SaveChanges();
            }
            return recipeEntry;  
        }

        public void SaveRecipe(Recipe recipe)
        {
            if(recipe.RecipeId == 0)
            {
                context.Recipes.Add(recipe);
            }
            else
            {
                Recipe recipeEntry = context.Recipes
                    .FirstOrDefault(rec => rec.RecipeId == recipe.RecipeId);
                if(recipeEntry != null)
                {
                    recipeEntry.RecipeName = recipe.RecipeName;
                    recipeEntry.Ingredients = recipe.Ingredients;
                    recipeEntry.RecipeInstructions = recipe.RecipeInstructions;
                }
            }
            context.SaveChanges();
        }
    }
}
