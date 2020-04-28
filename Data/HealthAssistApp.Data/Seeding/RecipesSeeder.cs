// <copyright file="RecipesSeeder.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models.Enums;

    public class RecipesSeeder : ISeeder
    {
        public RecipesSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            // Recipes
            if (dbContext.Recipes.Any())
            {
                return;
            }

            var recipes = new[]
            {
                (Name: "Chicken",
                Intructions: "Cut the chicken and boil it.",
                ImageUrgl: "https://mail.google.com/mail/?tab=im",
                Vegan: false,
                Vegetarian: false,
                PartOfMeal: PartOfMeal.MainMeal,
                GlycemicIndex: GlycemicIndex.Medium,
                Calories: 100),
                (Name: "Pork",
                Intructions: "Cut the pork chops. Put it in the oven, put some sauce on it.",
                ImageUrgl: "https://www.google.com/calendar?tab=ic",
                Vegan: false,
                Vegetarian: false,
                PartOfMeal: PartOfMeal.MainMeal,
                GlycemicIndex: GlycemicIndex.Medium,
                Calories: 100),
                (Name: "Burrito",
                Intructions: "Get minced meat. Put it in a pan, fill the burrito.",
                ImageUrgl: "https://www.google.com/url?sa=i&rct=j&q=&esrc=s&source=imgres&cd=&cad=rja&uact=8&ved=2ahUKEwjAvqD91YvpAhWNERQKHcDfCJEQjRx6BAgBEAQ&url=https%3A%2F%2Fwww.mccormick.com%2Ffrenchs%2Frecipes%2Fmain-dish%2Fcheeseburger-burritos&psig=AOvVaw2wTlztiFliAOUzz-8NiS40&ust=1588182222607644",
                Vegan: false,
                Vegetarian: false,
                PartOfMeal: PartOfMeal.MainMeal,
                GlycemicIndex: GlycemicIndex.Medium,
                Calories: 100),
                (Name: "Cereal",
                Intructions: "This recipe has a twist.",
                ImageUrgl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.nestle-cereals.com%2Fglobal%2Fen%2Frecipes-tips%2Frecipes%2Fmake-fitness-cereal&psig=AOvVaw2iZQDXgnn4J2mJQAYvbbSB&ust=1588182295876000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCPC-sJ_Wi-kCFQAAAAAdAAAAABAD",
                Vegan: false,
                Vegetarian: true,
                PartOfMeal: PartOfMeal.Snack,
                GlycemicIndex: GlycemicIndex.Medium,
                Calories: 100),
                (Name: "Fruit Salad",
                Intructions: "Cut the fruit in cubes, and place it in the bowls.",
                ImageUrgl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Flifemadesimplebakes.com%2Fhow-to-make-fruit-salad%2F&psig=AOvVaw39Bbes0ej1oU3YYnroVQfT&ust=1588182382740000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLCE9PfWi-kCFQAAAAAdAAAAABAJ",
                Vegan: true,
                Vegetarian: true,
                PartOfMeal: PartOfMeal.Snack,
                GlycemicIndex: GlycemicIndex.Medium,
                Calories: 100),
                (Name: "Healthy Sandwiches",
                Intructions: "Buy wholegrain bread. Smear it with peanutbutter",
                ImageUrgl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.msn.com%2Fen-us%2Ffoodanddrink%2Frecipes%2Fthe-surprising-ingredient-southerners-put-on-a-peanut-butter-sandwich%2Far-AAJxtbj&psig=AOvVaw2oHzJRS46BcurB3MaXyNOB&ust=1588182644810000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCNC78MnXi-kCFQAAAAAdAAAAABAD",
                Vegan: false,
                Vegetarian: true,
                PartOfMeal: PartOfMeal.Snack,
                GlycemicIndex: GlycemicIndex.Medium,
                Calories: 100),
            }.ToList();

            foreach (var recipe in recipes)
            {
                await dbContext.Recipes.AddAsync(new Models.Recipe
                {
                    Name = recipe.Name,
                    InstructionForPreparation = recipe.Intructions,
                    ImageUrl = recipe.ImageUrgl,
                    Vegan = recipe.Vegan,
                    Vegetarian = recipe.Vegetarian,
                    PartOfMeal = recipe.PartOfMeal,
                    GlycemicIndex = recipe.GlycemicIndex,
                    Calories = recipe.Calories,
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
