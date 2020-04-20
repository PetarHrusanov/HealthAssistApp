namespace HealthAssistApp.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>
    {

        public RecipeViewModel()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredients>();
            this.Ingredients = new HashSet<Ingredient>();
            //this.Ingredients = new List<Ingredient>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string InstructionForPreparation { get; set; }

        public string ImageUrl { get; set; }

        public bool Vegan { get; set; }

        public bool Vegetarian { get; set; }

        public int Calories { get; set; }

        public GlycemicIndex GlycemicIndex { get; set; }

        public ICollection<RecipeIngredients> RecipeIngredients { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
