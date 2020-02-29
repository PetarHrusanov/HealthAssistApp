namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;

    public class Meal: BaseDeletableModel<int>
    {

        public Meal()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public ICollection<Recipe> Recipes { get; set; }
    }
}