// <copyright file="RecipesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.RecipesViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class RecipesController : AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly IRecipesService recipesService;

        public RecipesController(ApplicationDbContext db, IRecipesService recipesService)
        {
            this.db = db;
            this.recipesService = recipesService;
        }

        // Recipes Logic
        public async Task<IActionResult> Index()
        {
            var recipes = await this.recipesService.GetAllAsync<RecipeAdminDetailsViewModel>();
            return this.View(recipes);
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipesAdminInputViewModel recipe)
        {
            await this.recipesService.CreateAsync(
                recipe.Name,
                recipe.InstructionForPreparation,
                recipe.ImageUrl,
                recipe.Vegan,
                recipe.Vegetarian,
                recipe.PartOfMeal,
                recipe.GlycemicIndex,
                recipe.Calories);

            this.TempData["CreatedRecipes"] = $"You have successfully created {recipe.Name}!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.recipesService.GetByIdAsyn<RecipesAdminModifyViewModel>(id);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,InstructionForPreparation,ImageUrl,Vegan,Vegetarian,PartOfMeal,GlycemicIndex,Calories")]RecipesAdminModifyViewModel recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.recipesService.ModifyAsync(
                     recipe.Id,
                     recipe.Name,
                     recipe.InstructionForPreparation,
                     recipe.ImageUrl,
                     recipe.Vegan,
                     recipe.Vegetarian,
                     recipe.PartOfMeal,
                     recipe.GlycemicIndex,
                     recipe.Calories);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await this.recipesService.GetByIdAsyn<RecipeAdminDetailsViewModel>(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception("A problem occurred while trying to edit this recipe.");
                    }
                }

                this.TempData["ModifiedRecipes"] = $"You have successfully modified {recipe.Name}!";

                return this.RedirectToAction("Index");
            }

            return View(recipe);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.recipesService.GetByIdAsyn<RecipeAdminDetailsViewModel>(id);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(recipe);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.recipesService.GetByIdAsyn<RecipeAdminDetailsViewModel>(id);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(recipe);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.recipesService.DeleteByIdAsync(id);

            this.TempData["DeletedRecipes"] = $"You have successfully deleted this recipe!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "Dashboard");
        }
    }
}
