namespace HealthAssistApp.Data.Models.WorkingOut
{
    using System;
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;

    public class WorkoutProgram : BaseModel<int>
    {
        public WorkoutProgram()
        {
            this.ExerciseWorkoutPrograms = new HashSet<ExerciseWorkoutProgram>();
        }

        public ICollection<ExerciseWorkoutProgram> ExerciseWorkoutPrograms { get; set; }
    }
}
