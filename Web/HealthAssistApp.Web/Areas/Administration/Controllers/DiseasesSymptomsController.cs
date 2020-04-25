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

        // Recipes Logic
        public async Task<IActionResult> Index()
        {
            // taka ne raboti da go obmislq
            var diseaseSymptomViewModels = await this.db.DiseaseSymptoms.Select(d => new DiseaseSymptomViewModel
            {
                DiseaseId = d.DiseaseId,
                DiseaseName = d.Disease.Name,
                SymptomId = d.SymptomId,
                SymptomName = d.Symptom.Description,
                IdS = $"{d.DiseaseId}X{d.SymptomId}",
            }).ToListAsync() as IEnumerable<DiseaseSymptomViewModel>;
            return this.View(diseaseSymptomViewModels);
        }

        public async Task<IActionResult> Create()
        {
            var diseasesInput = this.diseasesService.DiseasesDropDownMenu<DiseasesDropDownViewModel>();
            var symptomsInput = this.symptomsServices.SymptomsDropDownMenu<SymptomsDropDownViewModel>();

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
            var diseaseSymptomDb = new DiseaseSymptom
            {
                DiseaseId = diseaseSymptom.DiseaseId,
                SymptomId = diseaseSymptom.SymptomId,
            };

            await this.db.AddAsync(diseaseSymptomDb);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string idS)
        {
            if (idS == null)
            {
                return NotFound();
            }

            var separatedIdS = idS.Split("X");
            int diseaseId = int.Parse(separatedIdS[0]);
            int symptomsId = int.Parse(separatedIdS[1]);
            var diseaseSymptom = await this.db.DiseaseSymptoms
                .Where(d => d.DiseaseId == diseaseId & d.SymptomId == symptomsId)
                .Select(s => new DiseaseSymptomViewModel
                {
                    DiseaseId = s.DiseaseId,
                    DiseaseName = s.Disease.Name,
                    SymptomId = s.SymptomId,
                    SymptomName = s.Symptom.Description,
                    IdS = $"{s.DiseaseId}X{s.SymptomId}",
                }).FirstOrDefaultAsync();
            return this.View(diseaseSymptom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string idS)
        {
            var separatedIdS = idS.Split("X");
            int diseaseId = int.Parse(separatedIdS[0]);
            int symptomsId = int.Parse(separatedIdS[1]);
            var diseaseSymptom = await this.db.DiseaseSymptoms
                .Where(d => d.DiseaseId == diseaseId & d.SymptomId == symptomsId)
                .FirstOrDefaultAsync();
            this.db.DiseaseSymptoms.Remove(diseaseSymptom);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
