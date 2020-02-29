namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;

    public class FoodRegimen :BaseDeletableModel<string>
    {

        public FoodRegimen()
        {
            this.Meals = new HashSet<Meal>();
        }

        public ICollection<Meal> Meals { get; set; }
    }
}