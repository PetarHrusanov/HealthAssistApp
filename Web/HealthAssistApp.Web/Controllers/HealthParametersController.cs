// <copyright file="HealthParametersController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.HealthParameters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HealthParametersController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IHealthParametersService healthParametersService;

        public HealthParametersController(ApplicationDbContext db, IHealthParametersService healthParametersService)
        {
            this.db = db;
            this.healthParametersService = healthParametersService;
        }

        [Authorize]
        public IActionResult Modify(string userId)
        {
            var healthParameters = this.db.HealthParameters.Where(a => a.ApplicationUserId == userId).FirstOrDefault();
            return this.View(userId);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Modify(HealthParametersModifyModel healthParametersModify)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(healthParametersModify);
            }

            var user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.healthParametersService.ModifyAsync(
                healthParametersModify.Age,
                healthParametersModify.Weight,
                healthParametersModify.Height,
                user);

            return this.RedirectToAction("Index", "HealthDosier");
        }
    }
}
