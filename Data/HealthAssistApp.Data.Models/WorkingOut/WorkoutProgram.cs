namespace HealthAssistApp.Data.Models.WorkingOut
{
    using System;
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;

    public class WorkoutProgram : BaseModel<int>
    {
        public WorkoutProgram()
        {
            this.ExerciseWorkoutPrograms = new HashSet<ExerciseWorkoutProgram>();
        }

        public ExerciseComplexity ExerciseComplexity { get; set; }

        public ICollection<ExerciseWorkoutProgram> ExerciseWorkoutPrograms { get; set; }
    }
}
