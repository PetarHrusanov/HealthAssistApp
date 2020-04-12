// <copyright file="DashboardController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        private readonly ApplicationDbContext db;

        public DashboardController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Diseases()
        {
            return View(await this.db.Diseases.ToListAsync());
        }

        public async Task<IActionResult> CreateDisease()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDisease(Disease disease)
        {
            await this.db.AddAsync(disease);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Diseases");
        }

        public async Task<IActionResult> Recipes()
        {
            return View(await this.db.Recipes.ToListAsync());
        }

        public async Task<IActionResult> Exercises()
        {
            return View(await this.db.Exercises.ToListAsync());
        }
    }
}
