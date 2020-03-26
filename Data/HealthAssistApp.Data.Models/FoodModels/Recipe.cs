namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;

    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;

    public class Recipe: BaseModel<int>
    {
        public Recipe()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredients>();
        }

        public string Name { get; set; }

        public string InstructionForPreparation { get; set; }

        public string ImageUrl { get; set; }

        public bool Vegan { get; set; }

        public bool Vegetarian { get; set; }

        public PartOfMeal PartOfMeal { get; set; }

        public GlycemicIndex GlycemicIndex { get; set; }

        public int Calories { get; set; }

        public virtual ICollection<RecipeIngredients> RecipeIngredients { get; set; }

        public virtual ICollection<Meal> Breakfasts { get; set; }

        public virtual ICollection<Meal> Lunches { get; set; }

        public virtual ICollection<Meal> Diners { get; set; }
    }
}
