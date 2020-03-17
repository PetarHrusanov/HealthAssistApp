using System;
using System.ComponentModel.DataAnnotations;

namespace HealthAssistApp.Web.ViewModels.HealthParameters
{
    public class HealthParametersInputModel
    {
        [Required]
        [Range(8, 120, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Age { get; set; }

        [Required]
        [Range(30, 350, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Weight { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal Height { get; set; }
    }
}
