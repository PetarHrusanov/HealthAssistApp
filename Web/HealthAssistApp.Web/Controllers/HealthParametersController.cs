// <copyright file="HealthParametersController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HealthAssistApp.Data;
    using HealthAssistApp.Web.ViewModels.HealthParameters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HealthParametersController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HealthParametersController(ApplicationDbContext db)
        {
            this.db = db;
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

            var healthParameters = this.db.HealthDosiers
                .Where(a => a.ApplicationUserId == user)
                .Select(a => a.HealthParameters)
                .FirstOrDefault();

            healthParameters.Age = healthParametersModify.Age;
            healthParameters.Height = healthParametersModify.Height;
            healthParameters.Weight = healthParametersModify.Weight;
            healthParameters.WaterPerDay = healthParametersModify.Weight * 0.033m;
            healthParameters.BodyMassIndex = healthParametersModify.Weight / (healthParameters.Height * healthParameters.Height);

            await this.db.SaveChangesAsync();

            return this.RedirectToAction("Index", "HealthDosier");
        }
    }
}
