// <copyright file="HealthDosierController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HealthDosierController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly ApplicationUser applicationUser;

        public HealthDosierController(ApplicationDbContext db, ApplicationUser applicationUser)
        {
            this.db = db;
            this.applicationUser = applicationUser;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthDosier = this.db.HealthDosiers.Where(x => x.ApplicationUserId == userId).FirstOrDefault();

            if (healthDosier == null)
            {
                return this.RedirectToPage("Create");
            }
            else
            {
                return this.View();
            }
        }
    }
}
