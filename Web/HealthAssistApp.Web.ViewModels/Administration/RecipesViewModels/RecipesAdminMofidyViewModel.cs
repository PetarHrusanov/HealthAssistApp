// <copyright file="RecipesAdminInputViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.RecipesViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class RecipesAdminModifyViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(110)]
        public string Name { get; set; }

        [Required]
        [MaxLength(5000)]
        public string InstructionForPreparation { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public bool Vegan { get; set; }

        [Required]
        public bool Vegetarian { get; set; }

        public PartOfMeal PartOfMeal { get; set; }

        public GlycemicIndex GlycemicIndex { get; set; }

        [Required]
        public int Calories { get; set; }
    }
}
