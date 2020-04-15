// <copyright file="ExercisesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.WorkingOut;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class ExercisesController : AdministrationController
    {
        private readonly ApplicationDbContext db;

        public ExercisesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // Exercises Logic
        public async Task<IActionResult> Index()
        {
            return View(await this.db.Exercises.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            await this.db.AddAsync(exercise);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Exercises");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var exercise = await this.db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return this.NotFound();
            }

            return this.View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                this.db.Update(exercise);
                await this.db.SaveChangesAsync();
            }

            return this.RedirectToAction("Exercises");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var exercise = await this.db.Exercises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return this.NotFound();
            }

            return this.View(exercise);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var exercise = await this.db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return this.NotFound();
            }

            return this.View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var exercise = await this.db.Exercises.FindAsync(id);
            this.db.Exercises.Remove(exercise);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Exercises");
        }
    }
}
