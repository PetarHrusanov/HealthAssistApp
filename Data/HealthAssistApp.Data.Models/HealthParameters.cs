namespace HealthAssistApp.Data.Models
{
    using HealthAssistApp.Data.Common.Models;

    public class HealthParameters: BaseDeletableModel<int>
    {

        public int Age { get; set; }

        public int Weight { get; set; }

        public decimal Height { get; set; }

        public decimal BodyMassIndex { get; set; }

        public decimal WaterPerDay { get; set; } 

    }
}