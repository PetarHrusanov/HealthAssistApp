// <copyright file="ExerciseWorkoutProgram.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models.WorkingOut
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExerciseWorkoutProgram
    {
        [Required]
        public int ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }

        [Required]
        public int WorkoutProgramId { get; set; }

        public WorkoutProgram WorkoutProgram { get; set; }
    }
}
