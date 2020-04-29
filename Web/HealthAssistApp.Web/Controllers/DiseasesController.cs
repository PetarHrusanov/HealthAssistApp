// <copyright file="DiseasesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Diseases;
    using Microsoft.AspNetCore.Mvc;

    public class DiseasesController: BaseController
    {
        private readonly IDiseasesService diseasesService;
        private const int ItemsPerPage = 9;

        public DiseasesController(IDiseasesService diseasesService)
        {
            this.diseasesService = diseasesService;
        }

        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            var viewModel = new IndexDiseasesViewModel
            {
                Diseases = this.diseasesService
                .GetAllPaginatedAsync<DiseasIndexShortViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = await this.diseasesService.GetDiseasesCountAsync();
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

        public async Task<IActionResult> ByNameAsync(string name)
        {
            var viewModel = await this.diseasesService.GetByNameAsync<DiseaseViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
