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
        public async Task<IActionResult> ByUserIdAsync(string userId)
        {
            var allergiesOutput = await this.allergiesService.ViewByUserIdAsync<AllergiesViewModel>(userId);
            return this.View(allergiesOutput);
        }

        [Authorize]
        public async Task<IActionResult> ModifyAsync(string userId)
        {
            var userAllergies = await this.allergiesService.GetByUserIdAsync(userId);
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

        [Authorize]
        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "HealthDosier");
        }
    }
}
