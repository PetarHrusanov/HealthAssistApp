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
        private const int ItemsPerPage = 9;

        public RecipesController(IRecipesService recipesService, ApplicationDbContext dbContext)
        {
            this.recipesService = recipesService;
            this.dbContext = dbContext;
        }

        public async System.Threading.Tasks.Task<IActionResult> IndexAsync(int page = 1)
        {
            var viewModel = new IndexRecipesViewModel
            {
                Recipes = this.recipesService
                .GetAllPaginatedAsync<RecipeViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = await this.recipesService.GetRecipesCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async System.Threading.Tasks.Task<IActionResult> ByNameAsync(string name)
        {
            var viewModel = await this.recipesService.GetByNameAsync<RecipeViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
