using System;

namespace HealthAssistApp.Web.ViewModels.Allergies
{
    public class AllergiesViewModel
    {
        public string UserId { get; set; }

        public bool Milk { get; set; }

        public bool Eggs { get; set; }

        public bool Fish { get; set; }

        public bool Crustacean { get; set; }

        public bool TreeNuts { get; set; }

        public bool Peanuts { get; set; }

        public bool Wheat { get; set; }

        public bool Soybeans { get; set; }
    }
}
