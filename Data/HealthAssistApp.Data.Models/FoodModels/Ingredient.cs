namespace HealthAssistApp.Data.Models
{
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;

    public class Ingredient: BaseDeletableModel<int>
    {

        public string Name { get; set; }

        public int Calories { get; set; }

        public GlycemicIndex GlycemicIndex { get; set; }

        public Allergen Allergen { get; set; }

        public bool Vegan { get; set; }
        public bool Vegetarian { get; set;}

    }
}