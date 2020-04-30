﻿// <copyright file="FoodRegimensService.cs" company="HealthAssistApp">
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
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class FoodRegimensService : IFoodRegimensService
    {
        private readonly IRepository<FoodRegimen> foodRegimensRepository;
        private readonly IRepository<Recipe> recipeRepository;
        private readonly IRepository<Meal> mealsRepository;
        private readonly IRepository<HealthDosier> healthDosierRepository;

        public FoodRegimensService(
            IRepository<FoodRegimen> foodRegimensRepository,
            IRepository<Recipe> recipeRepository,
            IRepository<Meal> mealsRepository,
            IRepository<HealthDosier> healthDosierRepository)
        {
            this.foodRegimensRepository = foodRegimensRepository;
            this.recipeRepository = recipeRepository;
            this.mealsRepository = mealsRepository;
            this.healthDosierRepository = healthDosierRepository;
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

            List<Recipe> recipes = await this.recipeRepository.AllAsNoTracking()
                    .ToListAsync();

            var snacks = await this.recipeRepository
                .AllAsNoTracking()
                .Where(m => m.PartOfMeal == PartOfMeal.Snack)
                .ToListAsync();

            var main = await this.recipeRepository
                .AllAsNoTracking()
                .Where(m => m.PartOfMeal == PartOfMeal.MainMeal)
                .ToListAsync();

            var random = new Random();

            for (int i = 0; i < 31; i++)
            {

                //var snacks = await this.recipeRepository
                //    .AllAsNoTracking()
                //    .Where()
                int breakfast = random.Next(snacks.Count);
                int lunch = random.Next(main.Count);
                int diner = random.Next(main.Count);

                var breakfastId = snacks[breakfast].Id;
                var lunchId = main[lunch].Id;
                var dinerId = main[diner].Id;

                var meal = new Meal
                {
                    BreakfastId = breakfastId,
                    LunchId = lunchId,
                    DinerId = dinerId,
                    FoodRegimenId = foodRegimen.Id,
                };

                await this.mealsRepository.AddAsync(meal);
                await this.mealsRepository.SaveChangesAsync();
            }

            return foodRegimen.Id;
        }

        public async Task DeleteMealsById(int id)
        {
            var meals = await this.mealsRepository
                .AllAsNoTracking()
                .Where(f => f.FoodRegimenId == id)
                .ToListAsync();

            foreach (var item in meals)
            {
                this.mealsRepository.Delete(item);
                await this.mealsRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var foodRegimen = await this.foodRegimensRepository
                .All()
                .FirstOrDefaultAsync(f => f.Id == id);

            this.foodRegimensRepository.Delete(foodRegimen);

            await this.foodRegimensRepository.SaveChangesAsync();
        }

        public async Task<int> GetRegimenByHealthDosierIdAsync(string healthDosierId)
        {
            var foodRegimen = await this.healthDosierRepository
                .AllAsNoTracking()
                .Where(h => h.Id == healthDosierId)
                .Select(f => f.FoodRegimen)
                .FirstOrDefaultAsync();

            return foodRegimen.Id;
        }

        public IEnumerable<T> GetMealsByFoodRegimenId<T>(
            int foodRegimenId,
            int? take = null,
            int skip = 0)
        {
            var query = this.mealsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.FoodRegimenId == foodRegimenId)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public Recipe Recipe(List<Recipe> selectedRecipes, string mealType)
        {
            var recipes = selectedRecipes
                .Where(r => r.PartOfMeal.ToString() == mealType.ToString())
                .ToList();

            Random rnd = new Random();
            return recipes[rnd.Next(recipes.Count)];
        }
    }
}
