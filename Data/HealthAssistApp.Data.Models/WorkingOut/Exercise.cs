namespace HealthAssistApp.Data.Models.WorkingOut
{
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;


    public class Exercise : BaseModel<int>
    {

        public Exercise()
        {
            this.ExerciseWorkoutPrograms = new HashSet<ExerciseWorkoutProgram>();
        }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public ExerciseComplexity ExerciseComplexity { get; set; }

        public ICollection<ExerciseWorkoutProgram> ExerciseWorkoutPrograms { get; set; }

    }
}