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
    using HealthAssistApp.Web.ViewModels.Administration.SymptomsViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class SymptomsController : AdministrationController
    {
        private readonly ApplicationDbContext db;

        public SymptomsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var symptoms = await this.db.Symptoms.Select(s => new SymptomsIndexViewModel
            {
                SymptomId = s.Id,
                Description = s.Description,
                BodySystemId = s.BodySystemId,
                BodySystemName = s.BodySystem.Name,

            }).ToListAsync() as IEnumerable<SymptomsIndexViewModel>;

            return this.View(symptoms);
        }

        public async Task<IActionResult> Create()
        {
            var bodySystemsDrop = await this.db.BodySystems.Select(d => new BodySystemsDropDownViewModel
            {
                Id = d.Id,
                Name = d.Name,
            }).ToListAsync() as IEnumerable<BodySystemsDropDownViewModel>;

            var symptomsInput = new SymptomsInputViewModel
            {
                bodySystems = bodySystemsDrop,
            };

            return this.View(symptomsInput);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SymptomsInputViewModel symptomsInput)
        {
            var symptom = new Symptom
            {
                Description = symptomsInput.Description,
                BodySystemId = symptomsInput.BodySystemId,
            };

            await this.db.AddAsync(symptom);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var symptom = await this.db.Symptoms
                .FirstOrDefaultAsync(m => m.Id == id);
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

            var bodySystemsDrop = await this.db.BodySystems
                .Select(d => new BodySystemsDropDownViewModel
            {
                Id = d.Id,
                Name = d.Name,
            }).ToListAsync() as IEnumerable<BodySystemsDropDownViewModel>;

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
            Symptom symptom = await this.db.Symptoms
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            symptom.BodySystemId = symptomInput.BodySystemId;
            symptom.Description = symptomInput.Description;

            if (this.ModelState.IsValid)
            {
                this.db.Update(symptom);
                await this.db.SaveChangesAsync();
            }

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var symptomView = await this.db.Symptoms
                .Where(s => s.Id == id)
                .Select(s => new SymptomsIndexViewModel
                {
                    SymptomId = s.Id,
                    Description = s.Description,
                    BodySystemId = s.BodySystemId,
                    BodySystemName = s.BodySystem.Name,
                }).FirstOrDefaultAsync();

            if (symptomView == null)
            {
                return this.NotFound();
            }

            return this.View(symptomView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var symptom = await this.db.Symptoms.FindAsync(id);
            this.db.Symptoms.Remove(symptom);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
