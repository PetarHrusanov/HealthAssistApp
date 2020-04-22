﻿// <copyright file="DiseasesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DiseasesController : AdministrationController
    {
        private readonly ApplicationDbContext db;

        public DiseasesController(ApplicationDbContext db)
        {
            this.db = db;
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
        public async Task<IActionResult> Create(Disease disease)
        {
            await this.db.AddAsync(disease);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var disease = await this.db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return this.NotFound();
            }

            return this.View(disease);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Disease disease)
        {
            if (id != disease.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                this.db.Update(disease);
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

            var category = await this.db.Diseases
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var disease = await this.db.Diseases.FindAsync(id);
            this.db.Diseases.Remove(disease);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var disease = await this.db.Diseases.FindAsync(id);

            if (disease == null)
            {
                return this.NotFound();
            }

            return this.View(disease);
        }
    }
}
