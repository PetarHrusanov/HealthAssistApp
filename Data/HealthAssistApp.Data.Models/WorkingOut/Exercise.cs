// <copyright file="Exercise.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models.WorkingOut
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;

    public class Exercise : BaseModel<int>
    {
        public Exercise()
        {
            this.ExerciseWorkoutPrograms = new HashSet<ExerciseWorkoutProgram>();
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Instructions { get; set; }

        [Required]
        public ExerciseComplexity ExerciseComplexity { get; set; }

        public ICollection<ExerciseWorkoutProgram> ExerciseWorkoutPrograms { get; set; }
    }
}
