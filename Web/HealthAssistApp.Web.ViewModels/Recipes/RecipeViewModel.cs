namespace HealthAssistApp.Web.ViewModels.Recipes
{
    using System;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string InstructionForPreparation { get; set; }

        public string ImageUrl { get; set; }
    }
}
