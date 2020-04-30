// <copyright file="AdministrationTextsController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.AdminitrationTextFilesViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class AdministrationTextsController : AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly IAdministrationTextService administrationTextService;

        public AdministrationTextsController(
            ApplicationDbContext db,
            IAdministrationTextService administrationTextService)
        {
            this.db = db;
            this.administrationTextService = administrationTextService;
        }

        public async Task<IActionResult> Index()
        {
            var files = await this.administrationTextService.GetAllinViewModelAsync<AdminTextFilesDetailsViewModels>();
            return this.View(files);
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminTextFilesCreateViewModel file)
        {
            await this.administrationTextService.CreateAsync(
                file.Name,
                file.Content);

            this.TempData["CreateFile"] = $"You have successfully created {file.Name}!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var file = await this.administrationTextService.GetByIdAsync<AdminTextFilesModifyViewModel>(id);
            if (file == null)
            {
                return this.NotFound();
            }

            return this.View(file);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content")] AdminTextFilesModifyViewModel file)
        {
            if (id != file.Id)
            {
                return this.NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.administrationTextService.ModifyAsync(
                    file.Id,
                    file.Name,
                    file.Content);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await this.administrationTextService.GetByIdAsync<AdminTextFilesDetailsViewModels>(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception("A problem occurred while trying to edit this file.");
                    }
                }

                this.TempData["ModifiedExercise"] = $"You have successfully modified {file.Name}!";

                return this.RedirectToAction("Index");
            }

            return this.View(file);

                //await this.administrationTextService.ModifyAsync(
                //    file.Id,
                //    file.Name,
                //    file.Content);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var file = await this.administrationTextService.GetByIdAsync<AdminTextFilesDetailsViewModels>(id);
            if (file == null)
            {
                return this.NotFound();
            }

            return this.View(file);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var file = await this.administrationTextService.GetByIdAsync<AdminTextFilesDetailsViewModels>(id);
            if (file == null)
            {
                return this.NotFound();
            }

            return this.View(file);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.administrationTextService.DeleteAsync(id);

            this.TempData["DeletedExercise"] = $"You have successfully deleted this file!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "Dashboard");
        }
    }
}
