// <copyright file="RecipesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly ApplicationDbContext dbContext;

        public RecipesController(IRecipesService recipesService, ApplicationDbContext dbContext)
        {
            this.recipesService = recipesService;
            this.dbContext = dbContext;
        }

        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {
            var viewModel = new IndexRecipesViewModel
            {
                Recipes = await this.recipesService.GetAllAsync<RecipeViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var viewModel =
                this.recipesService.GetByName<RecipeViewModel>(name);
            //var ingredientsList = this.dbContext.Recipes.Where(x => x.Name == viewModel.Name).Select(x => x.Ingredients).ToList();
            //viewModel.Ingredients = ingredientsList;
            return this.View(viewModel);
        }
    }
}
