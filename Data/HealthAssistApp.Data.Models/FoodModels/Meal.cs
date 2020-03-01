namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;

    public class Meal: BaseModel<int>
    {

        public Meal()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public ICollection<Recipe> Recipes { get; set; }
    }
}