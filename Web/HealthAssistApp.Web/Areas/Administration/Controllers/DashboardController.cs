// <copyright file="DashboardController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        private readonly ApplicationDbContext db;

        public DashboardController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        // Diseases Logic
        // da obmislq dali vav view da ima pipane na datite
        public async Task<IActionResult> Diseases()
        {
            return View(await this.db.Diseases.ToListAsync());
        }

        public async Task<IActionResult> CreateDisease()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDisease(Disease disease)
        {
            await this.db.AddAsync(disease);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Diseases");
        }

        public async Task<IActionResult> EditDisease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = await this.db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDisease(int id, Disease disease)
        {
            if (id != disease.Id)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                this.db.Update(disease);
                await this.db.SaveChangesAsync();
            }

            return RedirectToAction("Diseases");
        }

        public async Task<IActionResult> DeleteDisease(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.db.Diseases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public async Task<IActionResult> DetailsDisease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = await this.db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var disease = await this.db.Diseases.FindAsync(id);
            this.db.Diseases.Remove(disease);
            await this.db.SaveChangesAsync();
            return RedirectToAction("Diseases");
        }

        // Recipes Logic
        public async Task<IActionResult> Recipes()
        {
            return View(await this.db.Recipes.ToListAsync());
        }

        public async Task<IActionResult> CreateRecipes()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecipes(Recipe recipes)
        {
            await this.db.AddAsync(recipes);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Recipes");
        }

        public async Task<IActionResult> EditRecipes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await this.db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecipes(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                this.db.Update(recipe);
                await this.db.SaveChangesAsync();
            }

            return RedirectToAction("Recipes");
        }

        public async Task<IActionResult> DetailsRecipes(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.db.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        public async Task<IActionResult> DeleteRecipes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await this.db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecipes(int id)
        {
            var recipe = await this.db.Recipes.FindAsync(id);
            this.db.Recipes.Remove(recipe);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Recipes");
        }

        // Exercises Logic
        public async Task<IActionResult> Exercises()
        {
            return View(await this.db.Exercises.ToListAsync());
        }
    }
}
