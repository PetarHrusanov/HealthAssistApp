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
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Allergies;
    using HealthAssistApp.Web.ViewModels.Diseases;
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

            var healthDosier = await this.healthDosiersService.GetByUserIdAsync(userId);

            if (healthDosier == null)
            {
                return this.RedirectToAction("HealthParametersInput");
            }

            var allergies = await this.allergiesService.GetByUserIdAsync(userId);

            // Health Parameters Logic
            var healthParameters = this.healthParametersService.ViewByUserId<HealthParametersViewModel>(userId);

            // da pomislq dali moje da se izkara v Service
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

            // da pomislq dali moje da sraboti
            // var diseasesForIndex = this.diseasesService.GetByHealthDosier<DiseaseViewModel>(userId) as ICollection<DiseaseViewModel>;

            // Health Dosier View
            var bodyMassIndex = healthParameters.BodyMassIndex;

            var nutritionalStatus = this.healthParametersService.NutritionalStatusByBodyMassIndex(bodyMassIndex);

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
                Id = healthDosier.Id,
            };

            return this.View(healthDosierView);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var healthDosier = await this.healthDosiersService.GetViewByIdAsync<HealthDosierOverview>(id);

            return this.View(healthDosier);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthDosierId = await this.healthDosiersService.GetIdByUserId(userId);
            var foodRegimenId = await this.foodRegimensService.GetRegimenByHealthDosierIdAsync(healthDosierId);
            var workoutId = await this.workOutsService.GetProgramIdByHealthDosierIdAsync(healthDosierId);
            await this.diseasesService.DeleteHealthDosierDiseasesByHealthIdAsync(healthDosierId);
            await this.healthDosiersService.UserSideDeleteAsync(healthDosierId);
            await this.healthParametersService.UserSideDeleteUserIdAsync(userId);
            await this.allergiesService.DeleteByUserIdAsync(userId);
            await this.foodRegimensService.DeleteMealsById(foodRegimenId);
            await this.foodRegimensService.DeleteByIdAsync(foodRegimenId);
            await this.workOutsService.DeleteWorkoutProgramAsync(workoutId);
            await this.symptomsServices.DeleteUserSymptomsAsync(userId);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> HealthParametersInput()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthParamCheckModel = await this.healthParametersService.GetByUserIdAsync(userId);
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

            this.TempData["HealthParametersSuccess"] = "HealthAssisApp has successfully stored your health parameters!";

            return this.RedirectToAction("AllergiesInput");
        }

        [Authorize]
        public async Task<IActionResult> AllergiesInput()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allergiesCheck = await this.allergiesService.GetByUserIdAsync(userId);
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

            this.TempData["AllergiesInputSuccess"] = "HealthAssisApp has successfully stored your allergies!";

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

        [Authorize]
        public async Task<IActionResult> DiseaseTest(string system)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userSymptoms = await this.symptomsServices.GetSystemNameFromUserId(userId);

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
                this.TempData["DiseasesCompleted"] = "You have successfully gone through all the systems!";

                return this.RedirectToAction("HealthDosierFinalising");
            }

            return this.RedirectToAction("DiseaseTest", "HealthDosier", new { system = nextSystem });
        }

        [Authorize]
        public async Task<IActionResult> HealthDosierFinalising()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthDosier = await this.healthDosiersService.GetByUserIdAsync(userId);
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
            var healthDosierCheck = await this.healthDosiersService.GetByUserIdAsync(userId);
            if (healthDosierCheck != null)
            {
                return this.RedirectToAction("Index");
            }

            var healthParameters = await this.healthParametersService.GetByUserIdAsync(userId);
            var allergies = await this.allergiesService.GetByUserIdAsync(userId);

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

            this.TempData["HealthDosierSuccess"] = "You have successfully created your Health Dosier!";

            return this.RedirectToAction("Index");
        }
    }
}
