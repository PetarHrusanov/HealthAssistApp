namespace HealthAssistApp.Data.Models.FoodModels
{
    using System;

    using HealthAssistApp.Data.Common.Models;

    public class Allergies : BaseModel<int>
    {
        public bool Milk { get; set; }

        public bool Eggs { get; set; }

        public bool Fish { get; set; }

        public bool Crustacean { get; set; }

        public bool TreeNuts { get; set; }

        public bool Peanuts { get; set; }

        public bool Wheat { get; set; }

        public bool Soybeans { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser {get;set;}

    }
}
