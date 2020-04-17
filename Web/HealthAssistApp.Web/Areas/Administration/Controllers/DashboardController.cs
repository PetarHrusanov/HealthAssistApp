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

        // da obmislq dali vav view da ima pipane na datite
        public async Task<IActionResult> Diseases()
        {
            return this.RedirectToAction("Index", "Diseases");
        }

        public async Task<IActionResult> Exercises()
        {
            return this.RedirectToAction("Index", "Exercises");
        }

        public async Task<IActionResult> Recipes()
        {
            return this.RedirectToAction("Index", "Recipes");
        }

        public async Task<IActionResult> BodySystems()
        {
            return this.RedirectToAction("Index", "BodySystems");
        }

        public async Task<IActionResult> Symptoms()
        {
            return this.RedirectToAction("Index", "Symptoms");
        }

        public async Task<IActionResult> DiseasesSymptoms()
        {
            return this.RedirectToAction("Index", "DiseasesSymptoms");
        }
    }
}
