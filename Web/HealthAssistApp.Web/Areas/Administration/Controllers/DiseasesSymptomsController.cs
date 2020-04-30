// <copyright file="RecipesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration;
    using HealthAssistApp.Web.ViewModels.Administration.DiseasesSymptomsViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DiseasesSymptomsController : AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly IDiseasesService diseasesService;
        private readonly ISymptomsServices symptomsServices;

        public DiseasesSymptomsController(
            ApplicationDbContext db,
            IDiseasesService diseasesService,
            ISymptomsServices symptomsServices)
        {
            this.db = db;
            this.diseasesService = diseasesService;
            this.symptomsServices = symptomsServices;
        }

        public async Task<IActionResult> Index()
        {
            var diseaseSymptomViewModels = await this.diseasesService
                .GetAllDiseaseSymptomsAsync<DiseaseSymptomViewModel>();

            return this.View(diseaseSymptomViewModels);
        }

        public async Task<IActionResult> Create()
        {
            var diseasesInput = await this.diseasesService.
                DiseasesDropDownMenuAsync<DiseasesDropDownViewModel>();

            var symptomsInput = this.symptomsServices
                .SymptomsDropDownMenu<SymptomsDropDownViewModel>();

            var inputDiseaseSymptom = new DiseaseSymptomInputViewModel
            {
                diseases = diseasesInput,
                symptoms = symptomsInput,
            };

            return this.View(inputDiseaseSymptom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiseaseSymptomInputViewModel diseaseSymptom)
        {
            await this.diseasesService.CreateDiseaseSymptomAsync(
                diseaseSymptom.DiseaseId,
                diseaseSymptom.SymptomId);

            this.TempData["CreateDiseaseSymptom"] = $"You have successfully created this relation!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string idS)
        {
            if (idS == null)
            {
                return NotFound();
            }

            var diseaseSymptom = await this.diseasesService
                .GetDiseaseSymptomAsync<DiseaseSymptomViewModel>(idS);

            return this.View(diseaseSymptom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string idS)
        {
            await this.diseasesService.DeleteDiseaseSymptomAsync(idS);

            this.TempData["DeleteDiseaseSymptom"] = $"You have successfully deleted this relation!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "Dashboard");
        }
    }
}
