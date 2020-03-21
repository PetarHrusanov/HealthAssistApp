// <copyright file="HealthDosierController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Web.ViewModels.Allergies;
    using HealthAssistApp.Web.ViewModels.HealthParameters;
    using HealthAssistApp.Web.ViewModels.Systems;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class HealthDosierController : BaseController
    {
        private readonly ApplicationDbContext db;
        public IList<string> systemsForTests;
        private readonly IList<SymptomsForSystems> symptomsForSystems;
        //da pomislq dali da go ostavq
        //private readonly HealthDosier healthDosier;

        public HealthDosierController(ApplicationDbContext db)
        {
            this.db = db;
            this.systemsForTests = this.db.BodySystems.Select(b => b.Name).ToList();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthDosier = await this.db.HealthDosiers.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();

            if (healthDosier == null)
            {
                return this.RedirectToAction("HealthParametersInput");
            }

            //if (healthDosier.HealthParameters == null)
            //{
            //    await this.HealthParameters();
            //}

            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> HealthParametersInput()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthParamCheckModel = await this.db.HealthParameters.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            if (healthParamCheckModel != null)
            {
                return this.RedirectToAction("AllergiesInput");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HealthParametersInput(HealthParametersInputModel healthParameters)
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

            return this.RedirectToAction("AllergiesInput");
        }

        [Authorize]
        public async Task<IActionResult> AllergiesInput()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthParamCheckModel = await this.db.Allergies.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            if (healthParamCheckModel != null)
            {
                return RedirectToAction("DiseaseTest", "HealthDosier", new { symptom = systemsForTests[0] });

                //return RedirectToAction("DiseaseTest");
                //return RedirectToAction("DiseaseTest", "HealthDosier", new { @id = systemsForTests[0].ToString() });
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AllergiesInput(AllergiesInputModel allergiesInput)
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

            return this.RedirectToAction("DiseaseTest", "HealthDosier", new { symptom = systemsForTests[0] });

            //return RedirectToAction("DiseaseTest");
            //return RedirectToAction("DiseaseTest", "HealthDosier", new { @id = systemsForTests[0] });
            //return RedirectToAction("DiseaseTest", "HealthDosier", symptomsForSystems[0]);
            //return this.RedirectToAction("Allergies");
            //return this.Redirect($"/HealthDosier/Allergies/{healthParametersForDb.Id}");
        }

        [Authorize]
        public async Task<IActionResult> DiseaseTest(string symptom)
        {
            SystemsWithSymptomsQuestionnaire bodySystem = this.db.BodySystems.Where(b => b.Name == symptom).Select(b => new SystemsWithSymptomsQuestionnaire
            {
                Name = b.Name,
                Symptoms = b.Symptoms.Select(sy => new SymptomsForSystems
                {
                    Description = sy.Description,
                }).ToList(),
            }).FirstOrDefault();

            return this.View(bodySystem);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DiseaseTest(SystemsWithSymptomsQuestionnaire systems)
        {
            foreach (var item in systems.Symptoms)
            {
                if (item.Selected == true)
                {
                    this.symptomsForSystems.Add(item);
                }
            }

            this.symptomsForSystems.Remove(this.symptomsForSystems[0]);

            return this.RedirectToAction("DiseaseTest");
        }
    }
}
