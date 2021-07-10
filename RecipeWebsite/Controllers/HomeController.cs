using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsite.Models;

namespace RecipeWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public HomeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public ViewResult Index() => View();

    }
}
