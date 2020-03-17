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
                //var halthDosier = new HealthDosier
                //{
                //    ApplicationUserId = userId,
                //    FoodRegimenId = 1,
                //    AllergiesId = 1,
                //    HealthParametersId = 2,
                //};
                //await this.db.HealthDosiers.AddAsync(healthDosier);
                //await this.db.SaveChangesAsync();
                //await this.HealthParameters();
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
        public async Task<IActionResult> HealthParameters(InputHealthParameters healthParameters)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(healthParameters);
            }

            var healthParametersForDb = new HealthParameters
            {
                Age = healthParameters.Age,
                Height = healthParameters.Height,
                Weight = healthParameters.Weight,
                WaterPerDay = healthParameters.Weight * 0.33M,
                BodyMassIndex = 703 * (healthParameters.Weight / healthParameters.Height),
            };

            this.db.HealthParameters.Add(healthParametersForDb);
            await this.db.SaveChangesAsync();

            return this.Redirect("/HealthDosier/Success");
            //return this.Redirect($"/HealthDosier/Allergies/{healthParametersForDb.Id}");
        }

        public async Task<IActionResult> Allergies(int allergiesId)
        {
            return this.View(allergiesId);
        }
    }
}
