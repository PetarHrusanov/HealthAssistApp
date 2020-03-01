namespace HealthAssistApp.Data.Models
{
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;


    public class Exercise: BaseModel<int>
    {

        public string Name { get; set; }

        public string Instructions { get; set; }

        public ExerciseComplexity ExerciseComplexity { get; set; }

    }
}