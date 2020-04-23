// <copyright file="BodySystemsController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Services.Data.BodySystems;
    using HealthAssistApp.Web.ViewModels.BodySystems;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class BodySystemsController : AdministrationController
    {
        private readonly ApplicationDbContext db;
        private readonly IBodySystemsService bodySystemsService;

        public BodySystemsController(ApplicationDbContext db, IBodySystemsService bodySystemsService)
        {
            this.db = db;
            this.bodySystemsService = bodySystemsService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.db.BodySystems.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BodySystemsInputViewModel bodySystem)
        {
            await this.bodySystemsService.CreateAsync(bodySystem.Name);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var bodySystem = await this.db.BodySystems.FindAsync(id);
            if (bodySystem == null)
            {
                return this.NotFound();
            }

            return this.View(bodySystem);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var bodySystem = this.bodySystemsService.GetById<BodySystemOverviewViewModel>(id);
            if (bodySystem == null)
            {
                return this.NotFound();
            }

            return this.View(bodySystem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BodySystem bodySystem)
        {
            if (id != bodySystem.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.bodySystemsService.ModifyAsync(bodySystem.Id, bodySystem.Name);

                //if (bodySystem.ModifiedOn!=null)
                //{
                //    var modify = bodySystem.ModifiedOn;
                //}

                //await this.bodySystemsService.ModifyAsync(
                //    bodySystem.Id,
                //    bodySystem.Name,);

                //this.db.Update(bodySystem);
                //await this.db.SaveChangesAsync();
            }

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var bodySystem = this.bodySystemsService.GetById<BodySystemOverviewViewModel>(id);
            if (bodySystem == null)
            {
                return this.NotFound();
            }

            return this.View(bodySystem);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.bodySystemsService.DeleteByIdAsync(id);
            return this.RedirectToAction("Index");
        }

    }
}
