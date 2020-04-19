// <copyright file="FoodRegimensService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class FoodRegimensService : IFoodRegimensService
    {
        private readonly IRepository<FoodRegimen> foodRegimensRepository;
        private readonly IRepository<Recipe> recipeRepository;
        private readonly IRepository<Meal> mealsRepository;

        public FoodRegimensService(
            IRepository<FoodRegimen> foodRegimensRepository,
            IRepository<Recipe> recipeRepository,
            IRepository<Meal> mealsRepository)
        {
            this.foodRegimensRepository = foodRegimensRepository;
            this.recipeRepository = recipeRepository;
            this.mealsRepository = mealsRepository;
        }

        public async Task<int> CreateFoodRegimenAsync(
            bool milk,
            bool eggs,
            bool fish,
            bool crustacean,
            bool treenuts,
            bool peanuts,
            bool wheat,
            bool soybeans)
        {
            var foodRegimen = new FoodRegimen { };
            await this.foodRegimensRepository.AddAsync(foodRegimen);
            await this.foodRegimensRepository.SaveChangesAsync();

            for (int i = 0; i < 31; i++)
            {
                List<Recipe> recipes = await this.recipeRepository.All().ToListAsync();

                var breakfast = this.Recipe(recipes, "Snack");
                var lunch = this.Recipe(recipes, "MainMeal");
                var diner = this.Recipe(recipes, "MainMeal");

                var meal = new Meal
                {
                    BreakfastId = breakfast.Id,
                    LunchId = lunch.Id,
                    DinerId = diner.Id,
                    FoodRegimenId = foodRegimen.Id,
                };

                await this.mealsRepository.AddAsync(meal);
                await this.mealsRepository.SaveChangesAsync();
            }

            return foodRegimen.Id;
        }

        public Recipe Recipe(List<Recipe> selectedRecipes, string mealType)
        {
            var recipes = selectedRecipes.Where(r => r.PartOfMeal.ToString() == mealType.ToString()).ToList();

            Random rnd = new Random();
            return recipes[rnd.Next(recipes.Count)];
        }
    }
}
