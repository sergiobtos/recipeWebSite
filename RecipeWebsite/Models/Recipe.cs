using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecipeWebsite.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        [Required(ErrorMessage = "Please enter recipe's name.")]
        [MaxLength(30, ErrorMessage = "Name can not exceed 30 characters")]
        public string RecipeName { get; set; }
        [Required(ErrorMessage = "Please enter the ingredients.")]
        public string Ingredients { get; set; }
        [Required(ErrorMessage = "Please enter recipe instructions.")]
        public string RecipeInstructions { get; set; }

        public List<Review> Reviews { get; set; }

      
        
    }

   
}
