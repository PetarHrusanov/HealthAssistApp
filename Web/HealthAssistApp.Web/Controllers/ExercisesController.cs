// <copyright file="ExercisesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Exercises;
    using HealthAssistApp.Web.ViewModels.Workouts;
    using Microsoft.AspNetCore.Mvc;

    public class ExercisesController : BaseController
    {
        private readonly IWorkOutsService workOutsService;
        private const int ItemsPerPage = 5;

        public ExercisesController(IWorkOutsService workOutsService)
        {
            this.workOutsService = workOutsService;
        }

        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            var viewModel = new ExercisesIndexViewModel
            {
                Exercises = this.workOutsService
                .GetAllPaginatedAsync<ExercisesWorkoutModel>(ItemsPerPage, (page - 1) * ItemsPerPage)
                as ICollection<ExercisesWorkoutModel>,
            };

            var count = await this.workOutsService.GetExercisesCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
