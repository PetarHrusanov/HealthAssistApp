// <copyright file="AllergiesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Linq;

    using HealthAssistApp.Data;
    using HealthAssistApp.Web.ViewModels.Allergies;
    using Microsoft.AspNetCore.Mvc;

    public class AllergiesController: BaseController
    {
        private readonly ApplicationDbContext db;

        public AllergiesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult ByUserId(string userId)
        {
            var userAllergies = this.db.Allergies.Where(a => a.ApplicationUserId == userId).FirstOrDefault();
            var allergiesOutput = new AllergiesViewModel
            {
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
    }
}
