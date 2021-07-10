using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RecipeWebsite.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                    new Recipe() { RecipeName = "Apple Butter Spice Cake", Ingredients = "Topping: 1 cup packed brown sugar, 1 teaspoon ground cinnamon,1/2 teaspoon ground nutmeg, 1/2 cup choppedpecans Cake: 2 cups all-purpose flour, 1 teaspoon baking powder, 1 teaspoon baking soda, 1/2 teaspoon salt, 1/2 cup butter, 1 cup white sugar.", RecipeInstructions = "\n1-Preheat oven to 350 degrees F (175 degrees C). Grease a 9x13-inch pan.\n2-Prepare the topping by mixing together the brown sugar, cinnamon, nutmeg, and chopped pecans." },
                    new Recipe() { RecipeName = "Homemade Melt Dark Chocolate", Ingredients = "1/2 cup coconut oil, 1/2 cup cocoa powder, 3 tablespoons honey, 1/2 teaspoon vanilla extract.", RecipeInstructions = "\nGently melt coconut oil in a saucepan over medium-low heat. Stir cocoa powder, honey, and vanilla extract into melted oil until well blended. Pour mixture into a candy mold or pliable tray. Refrigerate until chilled, about 1 hour." }
                    );
            }

            context.SaveChanges();
        }
    }
}
