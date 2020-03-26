namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;

    using HealthAssistApp.Data.Common.Models;

    public class Meal: BaseModel<int>
    {
        public int BreakfastId { get; set; }

        public virtual Recipe Breakfast { get; set; }

        public int LunchId { get; set; }

        public virtual Recipe Lunch { get; set; }

        public int DinerId { get; set; }

        public virtual Recipe Diner { get; set; }
    }
}
