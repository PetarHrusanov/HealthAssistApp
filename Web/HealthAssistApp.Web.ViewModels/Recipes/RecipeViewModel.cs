namespace HealthAssistApp.Web.ViewModels.Recipes
{
    using System;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string InstructionForPreparation { get; set; }

        public string ImageUrl { get; set; }

        public bool Vegan { get; set; }

        public bool Vegetarian { get; set; }

        public int Calories { get; set; }

        public GlycemicIndex GlycemicIndex { get; set; }

    }
}
