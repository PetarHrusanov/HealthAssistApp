// <copyright file="WorkoutsController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels.Workouts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutsController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly ApplicationDbContext db;
        private readonly IWorkOutsService workOutsService;

        public WorkoutsController(ApplicationDbContext db, IWorkOutsService workOutsService)
        {
            this.db = db;
            this.workOutsService = workOutsService;
        }

        [Authorize]
        public async Task<IActionResult> ByHealthDosierAsync(string healthDosierId, int page = 1)
        {
            var workOutId = await this.workOutsService.GetWorkoutProgramsByHealthDosierId(healthDosierId);

            var workoutProgram = new WorkoutProgramIndex { };
            workoutProgram.Exercises = this.workOutsService
                .GetExercisesByWorkoutId<ExercisesWorkoutModel>(workOutId, ItemsPerPage, (page - 1) * ItemsPerPage)
                as ICollection<ExercisesWorkoutModel>;

            var count = await this.workOutsService.GetExercisesCountByWorkoutId(workOutId);

            workoutProgram.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (workoutProgram.PagesCount == 0)
            {
                workoutProgram.PagesCount = 1;
            }

            workoutProgram.CurrentPage = page;

            workoutProgram.HealthDosierId = healthDosierId;

            if (workoutProgram == null)
            {
                return this.NotFound();
            }

            return this.View(workoutProgram);
        }

        [Authorize]
        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "HealthDosier");
        }
    }
}
