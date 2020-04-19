namespace HealthAssistApp.Web.ViewModels.HealthDosier
{
    using System;

    using HealthAssistApp.Data.Models.Enums;

    public class HealthDosierInputModel
    {
        public bool Smoker { get; set; }

        public bool DrinkAlcohol { get; set; }

        public ExerciseComplexity Complexity { get; set; }
    }
}
