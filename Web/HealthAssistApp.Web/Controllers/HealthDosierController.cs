// <copyright file="HealthDosierController.cs" company="HealthAssistApp">
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

            var healthDosier = this.db.HealthDosiers
                .Where(x => x.ApplicationUserId == userId)
                .FirstOrDefault();

            if (healthDosier == null)
            {
                return this.RedirectToAction("HealthParametersInput");
            }

            // trqbva da dobavq koncepciq za informaciq- ako body mass index-a e zle da se puska enum che e zle

            // da ima butoni za promqna

            // da opravq shemata s alergii da si e v otdelen controller
            //var allergiesOutput = new AllergiesViewModel
            //{
            //    Milk = allergies.Milk,
            //    Eggs = allergies.Eggs,
            //    Fish = allergies.Fish,
            //    Crustacean = allergies.Crustacean,
            //    TreeNuts = allergies.TreeNuts,
            //    Peanuts = allergies.Peanuts,
            //    Wheat = allergies.Wheat,
            //    Soybeans = allergies.Soybeans,
            //};

            var allergies = this.db.HealthDosiers
                .Where(a => a.ApplicationUserId == userId)
                .Select(a => a.Allergies)
                .FirstOrDefault();

            // Health Parameters Logic
            var healthParameters = this.db.HealthParameters
                .Where(a => a.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            var healthParametersOutput = new HealthParametersViewModel
            {
                Age = healthDosier.HealthParameters.Age,
                Weight = healthDosier.HealthParameters.Weight,
                Height = healthDosier.HealthParameters.Height,
                BodyMassIndex = healthDosier.HealthParameters.BodyMassIndex,
                WaterPerDay = healthDosier.HealthParameters.WaterPerDay,
            };

            // da dobavq ICollection s Diseases, koito da e null-able i to tam da orpavq neshtata 
            var diseaseId = this.db.HealthDosiers
                .Where(x => x.ApplicationUserId == userId)
                .Select(s => s.HealthDosierDiseases.Select(i => i.DiseaseId).FirstOrDefault())
                .FirstOrDefault();

            // Health Dosier View
            var healthDosierView = new HealthDosierOverview
            {
                AllergiesId = allergies.Id,
                DrinkAlcohol = healthDosier.DrinkAlcohol,
                Smoker = healthDosier.Smoker,
                HealthParameters = healthParametersOutput,
                WorkingOutProgramId = healthDosier.WorkoutProgramId,
                FoodRegimenId = healthDosier.FoodRegimenId,
                DiseaseId = diseaseId,
            };

            return this.View(healthDosierView);
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
            var healthParamCheckModel = await this.db.Allergies
                .Where(x => x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();
            if (healthParamCheckModel != null)
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
        private async Task<Recipe> RecipeAsync(List<Recipe> selectedRecipes, string mealType)
        {
            var recipes = selectedRecipes.Where(r => r.PartOfMeal.ToString() == mealType.ToString()).ToList();
            //Random rnd = new Random();
            //int r = rnd.Next(recipes.Count);

            // da go opravq
            return recipes[0];
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
            var healthDosierCheck = await this.db.HealthDosiers.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            if (healthDosierCheck != null)
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
            var healthDosierCheck = await this.db.HealthDosiers.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            if (healthDosierCheck != null)
            {
                return this.RedirectToAction("Index");
            }

            var healthParameters = await this.db.HealthParameters.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            var allergies = await this.db.Allergies.Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
            var workingOutProgram = new WorkoutProgram
            {
                ExerciseComplexity = inputModel.Complexity,
                ApplicationUserId = userId,
            };

            await this.db.WorkoutPrograms.AddAsync(workingOutProgram);
            await this.db.SaveChangesAsync();

            var selectedExercises = await this.db.Exercises
                .Where(e => e.ExerciseComplexity == inputModel.Complexity)
                .ToListAsync();

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

            // ne raboti da go opravq
            // var recipes = await this.db.Recipes
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Milk.Equals(allergies.Milk)))
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Crustacean.Equals(allergies.Crustacean)))
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Eggs.Equals(allergies.Eggs)))
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Fish.Equals(allergies.Fish)))
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Peanuts.Equals(allergies.Peanuts)))
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Soybeans.Equals(allergies.Soybeans)))
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.TreeNuts.Equals(allergies.TreeNuts)))
            //    .Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Wheat.Equals(allergies.Wheat)))
            //    .ToListAsync();

            var recipes = await this.db.Recipes.ToListAsync();

            var foodRegimen = new FoodRegimen { };
            await this.db.AddAsync(foodRegimen);
            await this.db.SaveChangesAsync();

            for (int i = 0; i < 31; i++)
            {
                var breakfast = await this.RecipeAsync(recipes, "Snack");
                var lunch = await this.RecipeAsync(recipes, "MainMeal");
                var diner = await this.RecipeAsync(recipes, "MainMeal");

                var meal = new Meal
                {
                    BreakfastId = breakfast.Id,
                    LunchId = lunch.Id,
                    DinerId = diner.Id,
                    FoodRegimenId = foodRegimen.Id,
                };

                await this.db.AddAsync(meal);
                await this.db.SaveChangesAsync();
            }

            var healthDosier = new HealthDosier
            {
                HealthParametersId = healthParameters.Id,
                FoodRegimenId = foodRegimen.Id,
                WorkoutProgramId = workingOutProgram.Id,
                Smoker = inputModel.Smoker,
                DrinkAlcohol = inputModel.DrinkAlcohol,
                AllergiesId = allergies.Id,
                ApplicationUserId = userId,
            };

            await this.db.HealthDosiers.AddAsync(healthDosier);
            await this.db.SaveChangesAsync();

            int symptomCount = default;

            var diseases = this.db.Diseases.ToList();
            var userSymptoms = this.db.UserSymptoms.Where(s => s.ApplicationUserId == userId).ToList();

            foreach (var disease in diseases)
            {
                foreach (var userSymptom in userSymptoms)
                {
                    var diseaseSymptoms = this.db.DiseaseSymptoms.Where(d => d.DiseaseId == disease.Id).Select(s => s.Symptom.Description).ToList();
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
                    var healthDosierDisease = new HealthDosierDisease
                    {
                        DiseaseId = disease.Id,
                        HealthDosierId = healthDosier.Id,
                    };
                    this.db.HealthDosierDiseases.Add(healthDosierDisease);
                    this.db.SaveChanges();
                    symptomCount = 0;
                }
            }

            //da opravq vzimaneto na User i tn da stava s metod 

            return this.RedirectToAction("Index");
        }
    }
}
