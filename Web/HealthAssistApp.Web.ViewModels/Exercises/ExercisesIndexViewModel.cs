// <copyright file="ExercisesIndexViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;

    using HealthAssistApp.Web.ViewModels.Workouts;

    public class ExercisesIndexViewModel
    {
        public ICollection<ExercisesWorkoutModel> Exercises { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public string HealthDosierId { get; set; }
    }
}
