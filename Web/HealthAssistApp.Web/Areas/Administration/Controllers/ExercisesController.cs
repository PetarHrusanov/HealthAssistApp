// <copyright file="ExercisesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.ExercisesViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class ExercisesController : AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly IWorkOutsService workOutsService;

        public ExercisesController(ApplicationDbContext db, IWorkOutsService workOutsService)
        {
            this.db = db;
            this.workOutsService = workOutsService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.db.Exercises.ToListAsync());
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
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var exercise = await this.workOutsService.GetByIdAsync<ExerciseAdminMofidyViewModel>(id);
            if (exercise == null)
            {
                return this.NotFound();
            }

            return this.View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExerciseAdminMofidyViewModel exercise)
        {
            if (id != exercise.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.workOutsService.ModifyAsync(
                    exercise.Id,
                    exercise.Name,
                    exercise.Instructions,
                    exercise.ExerciseComplexity);
            }

            return this.RedirectToAction("Index");
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
            return this.RedirectToAction("Index");
        }
    }
}
