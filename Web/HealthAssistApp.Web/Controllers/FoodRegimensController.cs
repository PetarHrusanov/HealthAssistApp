// <copyright file="FoodRegimensController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.FoodRegimens;
    using HealthAssistApp.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class FoodRegimensController : BaseController
    {
        private const int ItemsPerPage = 1;

        private readonly ApplicationDbContext db;
        private readonly IFoodRegimensService foodRegimenService;

        public FoodRegimensController(ApplicationDbContext db, IFoodRegimensService foodRegimenService)
        {
            this.db = db;
            this.foodRegimenService = foodRegimenService;
        }

        public IEnumerable<MealViewModel> Pagination(int foodRegimenId, int? take = null, int skip = 0)
        {
            var query = this.db.Meals
              .Where(f => f.FoodRegimenId == foodRegimenId)
              .OrderByDescending(m => m.CreatedOn)
              .Select(m => new MealViewModel
              {
                  BraekfastId = m.BreakfastId,
                  Breakfast = new RecipeViewModel
                  {
                      ImageUrl = m.Breakfast.ImageUrl,
                      InstructionForPreparation = m.Breakfast.InstructionForPreparation,
                      Id = m.Breakfast.Id,
                      Name = m.Breakfast.Name,
                      Vegan = m.Breakfast.Vegan,
                      Vegetarian = m.Breakfast.Vegetarian,
                      Calories = m.Breakfast.Calories,
                      GlycemicIndex = m.Breakfast.GlycemicIndex,
                  },
                  LunchId = m.LunchId,
                  Lunch = new RecipeViewModel
                  {
                      ImageUrl = m.Lunch.ImageUrl,
                      InstructionForPreparation = m.Lunch.InstructionForPreparation,
                      Id = m.Lunch.Id,
                      Name = m.Lunch.Name,
                      Vegan = m.Lunch.Vegan,
                      Vegetarian = m.Lunch.Vegetarian,
                      Calories = m.Lunch.Calories,
                      GlycemicIndex = m.Lunch.GlycemicIndex,
                  },
                  DinerId = m.DinerId,
                  Diner = new RecipeViewModel
                  {
                      ImageUrl = m.Diner.ImageUrl,
                      InstructionForPreparation = m.Diner.InstructionForPreparation,
                      Id = m.Diner.Id,
                      Name = m.Diner.Name,
                      Vegan = m.Diner.Vegan,
                      Vegetarian = m.Diner.Vegetarian,
                      Calories = m.Diner.Calories,
                      GlycemicIndex = m.Diner.GlycemicIndex,
                  },
              }).Skip(skip) as IQueryable<MealViewModel>;

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }

        [Authorize]
        public async Task<IActionResult> ByHealthDosierAsync(string healthDosierId, int page = 1)
        {
            var regimenId = await this.foodRegimenService.GetRegimenByHealthDosierIdAsync(healthDosierId);

            var foodRegimen = new FoodRegimenMealsIndex { };

            foodRegimen.Meals = this.Pagination(regimenId, ItemsPerPage, (page - 1) * ItemsPerPage) as ICollection<MealViewModel>;
            foodRegimen.PagesCount = (int)Math.Ceiling(31D / ItemsPerPage);

            if (foodRegimen.PagesCount == 0)
            {
                foodRegimen.PagesCount = 1;
            }

            foodRegimen.CurrentPage = page;
            foodRegimen.HealthDosierId = healthDosierId;

            if (foodRegimen == null)
            {
                return this.NotFound();
            }

            return this.View(foodRegimen);
        }

        [Authorize]
        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "HealthDosier");
        }
    }
}
