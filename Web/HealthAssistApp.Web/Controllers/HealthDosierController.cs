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
    using HealthAssistApp.Web.ViewModels.Allergies;
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

        // da pomislq dali da go ostavq

        // private readonly HealthDosier healthDosier;

        public HealthDosierController(ApplicationDbContext db)
        {
            this.db = db;
            this.SystemsForTests = this.db.BodySystems.Select(b => b.Name).ToList();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthDosier = await this.db.HealthDosiers
                .Where(x => x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            if (healthDosier == null)
            {
                return this.RedirectToAction("HealthParametersInput");
            }

            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> HealthParametersInput()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthParamCheckModel = await this.db.HealthParameters
                .Where(x => x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();
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
                return RedirectToAction("DiseaseTest", "HealthDosier", new { system = SystemsForTests[0] });
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

            return this.RedirectToAction("DiseaseTest", "HealthDosier", new { system = SystemsForTests[0] });
        }

        private string GetNext(IList<string> items, string curr)
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
                    string nextSystem = this.GetNext(this.SystemsForTests, system);
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
                        var userSymptom = new UserSymptoms
                        {
                            Description = item.Description,
                            SystemName = systems.Name,
                            ApplicationUserId = userId,
                        };

                        await this.db.UserSymptoms.AddAsync(userSymptom);
                        await this.db.SaveChangesAsync();
                    }
                }
            }

            if (systems.Symptoms.Count == 0)
            {
                var userSymptom = new UserSymptoms
                {
                    Description = "Nothing",
                    SystemName = systems.Name,
                    ApplicationUserId = userId,
                };

                await this.db.UserSymptoms.AddAsync(userSymptom);
                await this.db.SaveChangesAsync();
            }

            string nextSystem = this.GetNext(this.SystemsForTests, systems.Name);
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
            var healthDosierCheck = await this.db.HealthDosiers.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            if (healthDosierCheck != null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HealthDosierFinalising(HealthDosierInputModel parametersInputModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthDosierCheck = await this.db.HealthDosiers.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            if (healthDosierCheck != null)
            {
                return this.RedirectToAction("Index");
            }

            var healthParameters = this.db.HealthParameters.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            var allergies = await this.db.Allergies.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            var workingOutProgram = new WorkoutProgram
            {
                ExerciseComplexity = parametersInputModel.Complexity,
                ApplicationUserId = userId,
            };

            await this.db.WorkoutPrograms.AddAsync(workingOutProgram);
            await this.db.SaveChangesAsync();

            var selectedExercises = this.db.Exercises
                .Where(e => e.ExerciseComplexity == parametersInputModel.Complexity)
                .ToList();

            foreach (var exercise in selectedExercises)
            {
                var exerciseWorkoutProgram = new ExerciseWorkoutProgram
                {
                    ExerciseId = exercise.Id,
                    WorkoutProgramId = workingOutProgram.Id,
                };

                await this.db.ExerciseWorkoutPrograms.AddAsync(exerciseWorkoutProgram);
                await this.db.SaveChangesAsync();
            }

            // da proverq dali raboti
            var recipes = this.db.Recipes
                .Where(i => i.Ingredients.Any(i => i.Crustacean.Equals(allergies.Crustacean)))
                .Where(i => i.Ingredients.Any(i => i.Eggs.Equals(allergies.Eggs)))
                .Where(i => i.Ingredients.Any(i => i.Fish.Equals(allergies.Fish)))
                .Where(i => i.Ingredients.Any(i => i.Milk.Equals(allergies.Milk)))
                .Where(i => i.Ingredients.Any(i => i.Peanuts.Equals(allergies.Peanuts)))
                .Where(i => i.Ingredients.Any(i => i.Soybeans.Equals(allergies.Soybeans)))
                .Where(i => i.Ingredients.Any(i => i.TreeNuts.Equals(allergies.TreeNuts)))
                .Where(i => i.Ingredients.Any(i => i.Wheat.Equals(allergies.Wheat)))
                .ToListAsync();

            // var recipeс = this.db.Recipes.Where(i => i.Ingredients.Where(i => i.Crustacean == allergies.Crustacean
            //    && i.Eggs == allergies.Eggs
            //    && i.Fish == allergies.Fish
            //    && i.Milk == allergies.Milk
            //    && i.Peanuts == allergies.Peanuts
            //    && i.Soybeans == allergies.Soybeans
            //    && i.TreeNuts == allergies.TreeNuts
            //    && i.Wheat == allergies.Wheat))
            //    .ToListAsync();


            var healthDosier = new HealthDosier
            {
                HealthParameters = await healthParameters,
                Allergies = allergies,


            };

            //da opravq vzimaneto na User i tn da stava s metod 

            return this.View();
        }
    }
}
