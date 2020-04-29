// <copyright file="HealthDosiersController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.HealthDosierViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HealthDosiersController  :AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly IHealthDosiersService healthDosiersService;

        public HealthDosiersController(
            ApplicationDbContext db,
            IHealthDosiersService healthDosiersService)
        {
            this.db = db;
            this.healthDosiersService = healthDosiersService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.healthDosiersService.GetAllAsync<HealthDosierAdminOverviewViewModel>());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var healthDosier = await this.healthDosiersService.GetViewByIdAsync<HealthDosierAdminOverviewViewModel>(id);

            if (healthDosier == null)
            {
                return this.NotFound();
            }

            return this.View(healthDosier);
        }

        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "Dashboard");
        }
    }
}
