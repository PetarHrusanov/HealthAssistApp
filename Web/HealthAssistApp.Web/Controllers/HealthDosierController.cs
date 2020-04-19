﻿// <copyright file="HealthDosierController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Allergies;
    using HealthAssistApp.Web.ViewModels.Diseases;
    using HealthAssistApp.Web.ViewModels.Enums;
    using HealthAssistApp.Web.ViewModels.HealthDosier;
    using HealthAssistApp.Web.ViewModels.HealthParameters;
    using HealthAssistApp.Web.ViewModels.Systems;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class HealthDosierController : BaseController
    {
        private readonly ApplicationDbContext db;
        public IList<string> SystemsForTests;
        private readonly IDiseasesService diseasesService;
        private readonly IAllergiesService allergiesService;
        private readonly IHealthParametersService healthParametersService;
        private readonly ISymptomsServices symptomsServices;
        private readonly IHealthDosiersService healthDosiersService;
        private readonly IWorkOutsService workOutsService;
        private readonly IFoodRegimensService foodRegimensService;

        public HealthDosierController(
            ApplicationDbContext db,
            IDiseasesService diseasesService,
            IAllergiesService allergiesService,
            IHealthParametersService healthParametersService,
            ISymptomsServices symptomsServices,
            IHealthDosiersService healthDosiersService,
            IWorkOutsService workOutsService,
            IFoodRegimensService foodRegimensService)
        {
            this.db = db;
            this.SystemsForTests = this.db.BodySystems.Select(b => b.Name).ToList();
            this.diseasesService = diseasesService;
            this.allergiesService = allergiesService;
            this.healthParametersService = healthParametersService;
            this.symptomsServices = symptomsServices;
            this.healthDosiersService = healthDosiersService;
            this.workOutsService = workOutsService;
            this.foodRegimensService = foodRegimensService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthDosier = this.healthDosiersService.GetById(userId);

            if (healthDosier == null)
            {
                return this.RedirectToAction("HealthParametersInput");
            }

            var allergies = this.allergiesService.GetByUserId(userId);

            // Health Parameters Logic
            var healthParameters = this.healthParametersService.ViewByUserId<HealthParametersViewModel>(userId);

            // da go izkaram v Service
            var diseasesForIndex = this.db.HealthDosierDiseases
                .Where(h => h.HealthDosierId == healthDosier.Id)
                .Select(d => d.Disease).Select(dv => new DiseaseViewModel
                {
                    Id = dv.Id,
                    Name = dv.Name,
                    Description = dv.Description,
                    Advice = dv.Advice,
                    GlycemicIndex = dv.GlycemicIndex,
                    DiseaseSymptoms = dv.DiseaseSymptoms,
                }).ToList() as ICollection<DiseaseViewModel>;

            // Health Dosier View
            var bodyMassIndex = healthParameters.BodyMassIndex;

            NutritionalStatus nutritionalStatus = NutritionalStatus.Normal;

            if (bodyMassIndex < 18.5m)
            {
                nutritionalStatus = NutritionalStatus.Underweight;
            }
            else if (bodyMassIndex <= 24.9m)
            {
                nutritionalStatus = NutritionalStatus.Normal;
            }
            else if (bodyMassIndex <= 29.9m)
            {
                nutritionalStatus = NutritionalStatus.ObesityI;
            }
            else if (bodyMassIndex <= 34.9m)
            {
                nutritionalStatus = NutritionalStatus.ObesityI;
            }
            else if (bodyMassIndex <= 39.9m)
            {
                nutritionalStatus = NutritionalStatus.ObesityII;
            }
            else
            {
                nutritionalStatus = NutritionalStatus.ObesityIII;
            }

            var healthDosierView = new HealthDosierOverview
            {
                AllergiesId = allergies.Id,
                DrinkAlcohol = healthDosier.DrinkAlcohol,
                Smoker = healthDosier.Smoker,
                HealthParameters = healthParameters,
                WorkingOutProgramId = healthDosier.WorkoutProgramId,
                FoodRegimenId = healthDosier.FoodRegimenId,
                Diseases = diseasesForIndex,
                NutritionalStatus = nutritionalStatus,
                UserId = userId,
            };

            // da dobavq gledaneto na food Regimen i Workout Program 

            return this.View(healthDosierView);
        }

        [Authorize]
        public async Task<IActionResult> HealthParametersInput()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthParamCheckModel = this.healthParametersService.GetByUserId(userId);
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

            await this.healthParametersService.CreateAsync(
                healthParameters.Age,
                healthParameters.Weight,
                healthParameters.Height,
                userId);

            return this.RedirectToAction("AllergiesInput");
        }

        [Authorize]
        public async Task<IActionResult> AllergiesInput()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allergiesCheck = this.allergiesService.GetByUserId(userId);
            if (allergiesCheck != null)
            {
                return this.RedirectToAction("DiseaseTest", "HealthDosier", new { system = this.SystemsForTests[0] });
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

            await this.allergiesService.CreateAsync(
                allergiesInput.Milk,
                allergiesInput.Eggs,
                allergiesInput.Fish,
                allergiesInput.Crustacean,
                allergiesInput.TreeNuts,
                allergiesInput.Peanuts,
                allergiesInput.Wheat,
                allergiesInput.Soybeans,
                userId);

            return this.RedirectToAction("DiseaseTest", "HealthDosier", new { system = this.SystemsForTests[0] });
        }

        private async Task<string> GetNext(IList<string> items, string curr)
        {
            if (string.IsNullOrWhiteSpace(curr))
            {
                return "Empty";
            }

            var index = items.IndexOf(curr);
            if (index == -1)
            {
                return "Empty";
            }

            if (index + 1 >= items.Count)
            {
                return "Empty";
            }

            return items[(index + 1) % items.Count];
        }

        // DA GO OPRAVQ
        //private async Task<Recipe> RecipeAsync(List<Recipe> selectedRecipes, string mealType)
        //{
        //    var recipes = selectedRecipes.Where(r => r.PartOfMeal.ToString() == mealType.ToString()).ToList();
        //    //Random rnd = new Random();
        //    //int r = rnd.Next(recipes.Count);

        //    // da go opravq
        //    return recipes[0];
        //}

        [Authorize]
        public async Task<IActionResult> DiseaseTest(string system)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSymptoms = await this.db.UserSymptoms
                .Where(x => x.ApplicationUserId == userId)
                .Select(u => u.SystemName)
                .ToListAsync();

            if (userSymptoms != null)
            {
                if (userSymptoms.Contains(system))
                {
                    string nextSystem = await this.GetNext(this.SystemsForTests, system);
                    if (nextSystem == "Empty")
                    {
                        return this.Redirect("HealthDosierFinalising");
                    }

                    return this.RedirectToAction("DiseaseTest", "HealthDosier", new { system = nextSystem });
                }
            }

            SystemsWithSymptomsQuestionnaire bodySystem = this.db.BodySystems
                .Where(b => b.Name == system)
                .Select(b => new SystemsWithSymptomsQuestionnaire
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (systems.Symptoms.Count > 0)
            {
                foreach (var item in systems.Symptoms)
                {
                    if (item.Selected == true)
                    {
                        await this.symptomsServices.CreateUserSymptomAsync(item.Description, systems.Name, userId);
                    }
                }
            }

            if (systems.Symptoms.Count == 0)
            {
                await this.symptomsServices.CreateUserSymptomAsync("Nothing", systems.Name, userId);
            }

            string nextSystem = await this.GetNext(this.SystemsForTests, systems.Name);
            if (nextSystem == "Empty")
            {
                return this.RedirectToAction("HealthDosierFinalising");
            }

            return this.RedirectToAction("DiseaseTest", "HealthDosier", new { system = nextSystem });
        }

        [Authorize]
        public async Task<IActionResult> HealthDosierFinalising()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthDosier = this.healthDosiersService.GetById(userId);
            if (healthDosier != null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HealthDosierFinalising(HealthDosierInputModel inputModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthDosierCheck = this.healthDosiersService.GetById(userId);
            if (healthDosierCheck != null)
            {
                return this.RedirectToAction("Index");
            }

            var healthParameters = this.healthParametersService.GetByUserId(userId);
            var allergies = this.allergiesService.GetByUserId(userId);

            int workoutProgramId = await this.workOutsService.CreateWorkoutProgramAsync(inputModel.Complexity, userId);

            var recipes = await this.db.Recipes.ToListAsync();

            int foodRegimenId = await this.foodRegimensService.CreateFoodRegimenAsync(
                allergies.Milk,
                allergies.Eggs,
                allergies.Fish,
                allergies.Crustacean,
                allergies.TreeNuts,
                allergies.Peanuts,
                allergies.Wheat,
                allergies.Soybeans);

            string healthDosierId = await this.healthDosiersService.CreateHealthDosierAsync(
                healthParameters.Id,
                foodRegimenId,
                workoutProgramId,
                inputModel.Smoker,
                inputModel.DrinkAlcohol,
                allergies.Id,
                userId);

            int symptomCount = default;

            var diseases = this.db.Diseases.ToList();
            var userSymptoms = this.db.UserSymptoms.Where(s => s.ApplicationUserId == userId).ToList();

            foreach (var disease in diseases)
            {
                foreach (var userSymptom in userSymptoms)
                {
                    var diseaseSymptoms = this.db.DiseaseSymptoms
                        .Where(d => d.DiseaseId == disease.Id)
                        .Select(s => s.Symptom.Description)
                        .ToList();
                    if (diseaseSymptoms.Count != 0)
                    {
                        foreach (var diseaseSymptom in diseaseSymptoms)
                        {
                            if (diseaseSymptom == userSymptom.Description)
                            {
                                symptomCount++;
                            }
                        }
                    }
                }

                if (symptomCount == 4)
                {
                    await this.diseasesService.CreateHealthDosierDiseaseAsync(disease.Id, healthDosierId);
                    symptomCount = 0;
                }
            }

            //da opravq vzimaneto na User i tn da stava s metod 

            return this.RedirectToAction("Index");
        }
    }
}
