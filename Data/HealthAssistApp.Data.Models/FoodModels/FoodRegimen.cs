namespace HealthAssistApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.FoodModels;

    public class FoodRegimen : BaseModel<int>
    {
        public FoodRegimen()
        {
            this.Meals = new HashSet<Meal>();
        }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}
