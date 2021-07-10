using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsite.Models;

namespace RecipeWebsite.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        

        public  RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        
        public ViewResult RecipeList()
        {
            var model = _recipeRepository.GetAllRecipes();
            return View(model);
        }
        public ViewResult ViewRecipe(int recipeId)
        {
            Recipe recipe = _recipeRepository.GetRecipe(recipeId);
            return View(recipe);

        }
        [HttpGet]
        public ViewResult ReviewRecipe(int recipeId)
        {
            Review revTemp = new Review();
            revTemp.RecipeId = recipeId;

            ViewBag.recipe = _recipeRepository.GetRecipe(recipeId);

            return View(revTemp);
        }
        
        [HttpPost]
        public ViewResult ReviewRecipe(Review review)
        {
            _recipeRepository.AddReview(review);
            
            return View("ViewRecipe", _recipeRepository.GetRecipe(review.RecipeId));
        }

        [HttpGet]
        public ViewResult AddRecipe()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                Recipe newRecipe = _recipeRepository.Add(recipe);
                return RedirectToAction("ViewRecipe", newRecipe);
            }
            return View();
        }


        [HttpPost]
        public IActionResult Search(string input)
        {
            var recipes = from rec in _recipeRepository.Recipes
                          select rec;

            if (!String.IsNullOrEmpty(input))
            {
                recipes = recipes.Where(s => s.RecipeName.Contains(input) ||
                                             s.RecipeInstructions.Contains(input) ||
                                             s.Ingredients.Contains(input));
            }

            if(!recipes.Any())
            {
                TempData["message"] = $"Sorry, no recipes matching \"{input}\" were found. Please, try again.";
            }

            return View("RecipeList", recipes);
        }
        //[HttpPost]
        //public string Search(string input, bool notUsed)
        //{
        //    return "From [HttpPost]Index: filter on " + input;
        //}

    }
}
