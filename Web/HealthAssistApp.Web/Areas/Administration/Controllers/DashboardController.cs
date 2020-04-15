// <copyright file="DashboardController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DashboardController : AdministrationController
    {
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
            return this.RedirectToAction("Index", "Diseases");
        }

        public async Task<IActionResult> Exercises()
        {
            return this.RedirectToAction("Index", "Exercises");
        }

        // Recipes Logic
        public async Task<IActionResult> Recipes()
        {
            return this.View(await this.db.Recipes.ToListAsync());
        }

        public async Task<IActionResult> CreateRecipes()
        {
            return this.View();
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
                return this.NotFound();
            }

            var recipe = await this.db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecipes(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                this.db.Update(recipe);
                await this.db.SaveChangesAsync();
            }

            return this.RedirectToAction("Recipes");
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
                return this.NotFound();
            }

            return this.View(recipe);
        }

        public async Task<IActionResult> DeleteRecipes(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(recipe);
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
    }
}
