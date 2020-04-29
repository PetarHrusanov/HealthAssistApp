// <copyright file="RecipesAdminInputViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.RecipesViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using HealthAssistApp.Data.Models.Enums;

    public class RecipesAdminInputViewModel
    {
        [Required]
        [MaxLength(110)]
        public string Name { get; set; }

        [Required]
        [MaxLength(5000)]
        [DisplayName("Instruction For Preparation")]
        public string InstructionForPreparation { get; set; }

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        public bool Vegan { get; set; }

        [Required]
        public bool Vegetarian { get; set; }

        [Required]
        [DisplayName("Part of Meal")]
        public PartOfMeal PartOfMeal { get; set; }

        [DisplayName("Glycemic Index")]
        public GlycemicIndex GlycemicIndex { get; set; }

        [Required]
        public int Calories { get; set; }
    }
}
