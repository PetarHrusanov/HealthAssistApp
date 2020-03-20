namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;

    public class Ingredient: BaseModel<int>
    {

        public Ingredient()
        {
            RecipeIngredients = new HashSet<RecipeIngredients>();
        }

        public string Name { get; set; }

        public int Calories { get; set; }

        public GlycemicIndex GlycemicIndex { get; set; }

        public Allergen Allergen { get; set; }

        public virtual ICollection<RecipeIngredients> RecipeIngredients { get; set; }

        public bool Vegan { get; set; }
        public bool Vegetarian { get; set;}

    }
}