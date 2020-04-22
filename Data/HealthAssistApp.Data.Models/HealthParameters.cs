// <copyright file="HealthParameters.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HealthAssistApp.Data.Common.Models;

    public class HealthParameters: BaseDeletableModel<int>
    {
        [Required]
        public int Age { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public decimal Height { get; set; }

        [Required]
        public decimal BodyMassIndex { get; set; }

        [Required]
        public decimal WaterPerDay { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}