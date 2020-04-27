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

        public DiseasesController(IDiseasesService diseasesService)
        {
            this.diseasesService = diseasesService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var viewModel = new IndexDiseasesViewModel
            {
                Diseases = await this.diseasesService.GetAllAsync<DiseaseViewModel>(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> ByNameAsync(string name)
        {
            var viewModel = await this.diseasesService.GetByNameAsync<DiseaseViewModel>(name);
            return this.View(viewModel);
        }
    }
}
