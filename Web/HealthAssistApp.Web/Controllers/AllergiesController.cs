// <copyright file="AllergiesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Web.ViewModels.Allergies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AllergiesController: BaseController
    {
        private readonly ApplicationDbContext db;

        public AllergiesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        public IActionResult ByUserId(string userId)
        {
            var userAllergies = this.db.Allergies.Where(a => a.ApplicationUserId == userId).FirstOrDefault();
            var allergiesOutput = new AllergiesViewModel
            {
                UserId = userId,
                Milk = userAllergies.Milk,
                Crustacean = userAllergies.Crustacean,
                Eggs = userAllergies.Eggs,
                Fish = userAllergies.Fish,
                Wheat = userAllergies.Wheat,
                Peanuts = userAllergies.Peanuts,
                Soybeans = userAllergies.Soybeans,
                TreeNuts = userAllergies.TreeNuts,
            };

            return this.View(allergiesOutput);
        }

        [Authorize]
        public IActionResult Modify(string userId)
        {
            var userAllergies = this.db.Allergies.Where(a => a.ApplicationUserId == userId).FirstOrDefault();
            return this.View(userId);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Modify(AllergiesInputModel allergiesInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(allergiesInput);
            }

            var user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allergies = this.db.HealthDosiers
                .Where(a => a.ApplicationUserId == user)
                .Select(a => a.Allergies)
                .FirstOrDefault();

            allergies.Milk = allergiesInput.Milk;
            allergies.Eggs = allergiesInput.Eggs;
            allergies.Fish = allergiesInput.Fish;
            allergies.Crustacean = allergiesInput.Crustacean;
            allergies.TreeNuts = allergiesInput.TreeNuts;
            allergies.Peanuts = allergiesInput.Peanuts;
            allergies.Wheat = allergiesInput.Wheat;
            allergies.Soybeans = allergiesInput.Soybeans;

            await this.db.SaveChangesAsync();

            return this.RedirectToAction("ByUserId", "Allergies", new { userId = user });
        }
    }
}
