using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsite.Models;

namespace RecipeWebsite.Controllers
{
   [Authorize]
    public class AdminController : Controller
    {
        private IRecipeRepository repository;
        public AdminController(IRecipeRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            return View(repository.GetAllRecipes());
        }
        
        [HttpPost]
        public IActionResult Delete(int recipeId)
        {
            Recipe deletedRecipe = repository.Delete(recipeId);
            if(deletedRecipe != null)
            {
                TempData["message"] = $"{deletedRecipe.RecipeName} was deleted!";
            }
            return RedirectToAction("Index");
        }

        public ViewResult Edit(int recipeId) => View(repository.GetRecipe(recipeId));

        [HttpPost]
        public IActionResult Edit (Recipe recipe)
        {
            if(ModelState.IsValid)
            {
                repository.SaveRecipe(recipe);
                TempData["message"] = $"{recipe.RecipeName} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(recipe);
            }

        }
        public ViewResult Create() => View("Edit", new Recipe());


        
    }
}