namespace HealthAssistApp.Data.Models.WorkingOut
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;

    public class WorkoutProgram : BaseModel<int>
    {
        public WorkoutProgram()
        {
            this.ExerciseWorkoutPrograms = new HashSet<ExerciseWorkoutProgram>();
        }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get;set;}

        public ExerciseComplexity ExerciseComplexity { get; set; }

        public ICollection<ExerciseWorkoutProgram> ExerciseWorkoutPrograms { get; set; }
    }
}
