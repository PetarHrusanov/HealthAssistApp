// <copyright file="RecipesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;

    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public IActionResult Index()
        {

            var viewModel = new IndexRecipesViewModel
            {
                Recipes = this.recipesService.GetAll<RecipeViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var viewModel =
                this.recipesService.GetByName<RecipeViewModel>(name);
            return this.View(viewModel);
        }
    }
}
