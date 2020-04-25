// <copyright file="DiseasesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.DiseasesViewModel;
    using HealthAssistApp.Web.ViewModels.Diseases;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DiseasesController : AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly IDiseasesService diseaseasesService;

        public DiseasesController(ApplicationDbContext db, IDiseasesService diseaseasesService)
        {
            this.db = db;
            this.diseaseasesService = diseaseasesService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.db.Diseases.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiseaseInputViewModel disease)
        {
            await this.diseaseasesService.CreateAsync(
                disease.Name,
                disease.Description,
                disease.Advice,
                disease.GlycemicIndex);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var disease = await this.diseaseasesService.GetByIdAsync<DiseaseAdminModifyViewModel>(id);
            if (disease == null)
            {
                return this.NotFound();
            }

            return this.View(disease);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiseaseAdminModifyViewModel disease)
        {
            if (id != disease.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.diseaseasesService.ModifyDiseaseAsync(
                    disease.Id,
                    disease.Name,
                    disease.Description,
                    disease.Advice,
                    disease.IsDeleted);
            }

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var disease = await this.diseaseasesService.GetByIdAsync<DiseaseAdminDetailsViewModel>(id);

            if (disease == null)
            {
                return this.NotFound();
            }

            return this.View(disease);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.diseaseasesService.DeleteByIdAsync(id);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var disease = await this.diseaseasesService.GetByIdAsync<DiseaseAdminDetailsViewModel>(id);

            if (disease == null)
            {
                return this.NotFound();
            }

            return this.View(disease);
        }
    }
}
