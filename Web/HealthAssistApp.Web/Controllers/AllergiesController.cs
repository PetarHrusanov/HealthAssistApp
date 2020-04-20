// <copyright file="AllergiesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Allergies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AllergiesController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IAllergiesService allergiesService;

        public AllergiesController(ApplicationDbContext db, IAllergiesService allergiesService)
        {
            this.db = db;
            this.allergiesService = allergiesService;
        }

        [Authorize]
        public IActionResult ByUserId(string userId)
        {
            //var userAllergies = this.allergiesService.GetByUserId(userId);
            var allergiesOutput = this.allergiesService.ViewByUserId<AllergiesViewModel>(userId);
            return this.View(allergiesOutput);
        }

        [Authorize]
        public IActionResult Modify(string userId)
        {
            var userAllergies = this.allergiesService.GetByUserId(userId);
            return this.View(userId);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Modify(AllergiesInputModel allergiesInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(allergiesInput);
            }

            var user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.allergiesService.ModifyAsync(
                allergiesInput.Milk,
                allergiesInput.Eggs,
                allergiesInput.Fish,
                allergiesInput.Crustacean,
                allergiesInput.TreeNuts,
                allergiesInput.Peanuts,
                allergiesInput.Wheat,
                allergiesInput.Soybeans,
                user);

            return this.RedirectToAction("ByUserId", "Allergies", new { userId = user });
        }
    }
}
