// <copyright file="SymptomsController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Services.Data.BodySystems;
    using HealthAssistApp.Web.ViewModels.Administration.SymptomsViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class SymptomsController : AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly ISymptomsServices symptomsService;
        private readonly IBodySystemsService bodySystemsService;

        public SymptomsController(
            ApplicationDbContext db,
            ISymptomsServices symptomsService,
            IBodySystemsService bodySystemsService)
        {
            this.db = db;
            this.symptomsService = symptomsService;
            this.bodySystemsService = bodySystemsService;
        }

        public async Task<IActionResult> Index()
        {
            //var symptoms = await this.db.Symptoms.Select(s => new SymptomsIndexViewModel
            //{
            //    SymptomId = s.Id,
            //    Description = s.Description,
            //    BodySystemId = s.BodySystemId,
            //    BodySystemName = s.BodySystem.Name,
            //}).ToListAsync() as IEnumerable<SymptomsIndexViewModel>;

            var symptoms = await this.symptomsService.GetAllinViewModelAsync<SymptomsIndexViewModel>();

            return this.View(symptoms);
        }

        public async Task<IActionResult> Create()
        {
            var bodySystemsDrop = await this.bodySystemsService
                .BodySystemDropDownMenu<BodySystemsDropDownViewModel>();

            var symptomsInput = new SymptomsInputViewModel
            {
                bodySystems = bodySystemsDrop,
            };

            return this.View(symptomsInput);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SymptomsInputViewModel symptomsInput)
        {
            await this.symptomsService.CreateSymptomAsync(symptomsInput.Description, symptomsInput.BodySystemId);
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var symptom = await this.symptomsService.GetModelByIdAsync<SymptomsAdminDetailsViewModel>(id);
            if (symptom == null)
            {
                return this.NotFound();
            }

            return this.View(symptom);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var symptom = await this.db.Symptoms.FindAsync(id);
            if (symptom == null)
            {
                return this.NotFound();
            }

            var bodySystemsDrop = await this.bodySystemsService
                .BodySystemDropDownMenu<BodySystemsDropDownViewModel>();

            var symptomInput = new SymptomsInputViewModel
            {
                SymptomId = symptom.Id,
                Description = symptom.Description,
                BodySystemId = symptom.BodySystemId,
                bodySystems = bodySystemsDrop,
            };

            return this.View(symptomInput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SymptomsInputViewModel symptomInput)
        {
            if (this.ModelState.IsValid)
            {
                await this.symptomsService.ModifySymptomAsync(
                    symptomInput.SymptomId,
                    symptomInput.Description,
                    symptomInput.BodySystemId);
            }

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var symptomView = await this.symptomsService.GetModelByIdAsync<SymptomsIndexViewModel>(id);

            if (symptomView == null)
            {
                return this.NotFound();
            }

            return this.View(symptomView);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.symptomsService.DeleteSymptomAsync(id);
            return this.RedirectToAction("Index");
        }
    }
}
