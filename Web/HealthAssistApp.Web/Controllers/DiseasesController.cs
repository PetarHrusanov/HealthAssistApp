// <copyright file="DiseasesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;

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

        public IActionResult Index()
        {
            var viewModel = new IndexDiseasesViewModel
            {
                Diseases = this.diseasesService.GetAll<DiseaseViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var viewModel =
                this.diseasesService.GetByName<DiseaseViewModel>(name);
            return this.View(viewModel);
        }
    }
}
