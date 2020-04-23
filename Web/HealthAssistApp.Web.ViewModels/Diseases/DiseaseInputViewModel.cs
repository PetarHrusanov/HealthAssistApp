// <copyright file="DiseaseInputViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    using System.ComponentModel.DataAnnotations;

    using HealthAssistApp.Data.Models.Enums;

    public class DiseaseInputViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Advice { get; set; }

        public GlycemicIndex? GlycemicIndex { get; set; }
    }
}
