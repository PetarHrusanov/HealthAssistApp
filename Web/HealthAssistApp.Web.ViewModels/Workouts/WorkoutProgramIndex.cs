// <copyright file="WorkoutProgramIndex.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Workouts
{
    using System;
    using System.Collections.Generic;

    public class WorkoutProgramIndex
    {
        public ICollection<ExercisesWorkoutModel> Exercises { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public string HealthDosierId { get; set; }
    }
}
