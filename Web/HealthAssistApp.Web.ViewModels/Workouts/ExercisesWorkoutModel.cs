// <copyright file="ExercisesWorkoutModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Workouts
{
    using System.ComponentModel;

    using Ganss.XSS;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Mapping;

    public class ExercisesWorkoutModel : IMapFrom<Exercise>
    {
        public string Name { get; set; }

        public string Instructions { get; set; }

        [DisplayName("Instructions")]
        public string SanitizedInstructions
          => new HtmlSanitizer().Sanitize(this.Instructions);

        public ExerciseComplexity ExerciseComplexity { get; set; }
    }
}
