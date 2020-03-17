// <copyright file="HealthDosierController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Web.ViewModels.Allergies;
    using HealthAssistApp.Web.ViewModels.HealthParameters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class HealthDosierController : BaseController
    {
        private readonly ApplicationDbContext db;
        //da pomislq dali da go ostavq
        //private readonly HealthDosier healthDosier;

        public HealthDosierController(ApplicationDbContext db)
        {
            this.db = db;
            //da vidq dali da e taka
            //this.healtDosier = healthDosier;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthDosier = await this.db.HealthDosiers.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();

            if (healthDosier == null)
            {
                return this.Redirect("/HealthDosier/HealthParameters");
            }

            //if (healthDosier.HealthParameters == null)
            //{
            //    await this.HealthParameters();
            //}

            return this.View();
        }

        public async Task<IActionResult> HealthParameters()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HealthParameters(HealthParametersInputModel healthParameters)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(healthParameters);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthParametersForDb = new HealthParameters
            {
                Age = healthParameters.Age,
                Height = healthParameters.Height,
                Weight = healthParameters.Weight,
                WaterPerDay = healthParameters.Weight * 0.33M,
                BodyMassIndex = 703 * (healthParameters.Weight / healthParameters.Height),
                ApplicationUserId = userId,
            };

            this.db.HealthParameters.Add(healthParametersForDb);
            await this.db.SaveChangesAsync();

            return this.RedirectToAction("Allergies");
            //return this.Redirect($"/HealthDosier/Allergies/{healthParametersForDb.Id}");
        }

        [Authorize]
        public async Task<IActionResult> Allergies(int allergiesId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Allergies(AllergiesInputModel allergiesInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(allergiesInput);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allergiesInputForDb = new Allergies
            {
                Milk = allergiesInput.Milk,
                Eggs = allergiesInput.Eggs,
                Fish = allergiesInput.Fish,
                Crustacean = allergiesInput.Crustacean,
                TreeNuts = allergiesInput.TreeNuts,
                Peanuts = allergiesInput.Peanuts,
                Wheat = allergiesInput.Wheat,
                Soybeans = allergiesInput.Soybeans,
                ApplicationUserId = userId,
            };

            this.db.Allergies.Add(allergiesInputForDb);
            await this.db.SaveChangesAsync();

            return this.Redirect("/HealthDosier/Success");

            //return this.RedirectToAction("Allergies");
            //return this.Redirect($"/HealthDosier/Allergies/{healthParametersForDb.Id}");
        }
    }
}
