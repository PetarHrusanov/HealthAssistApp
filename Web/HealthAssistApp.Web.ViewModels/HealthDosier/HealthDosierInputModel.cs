using System;
using HealthAssistApp.Data.Models.Enums;

namespace HealthAssistApp.Web.ViewModels.HealthDosier
{
    public class HealthDosierInputModel
    {
        public bool Smoker { get; set; }

        public bool DrinkAlcohol { get; set; }

        public ExerciseComplexity Complexity { get; set; }
    }
}
