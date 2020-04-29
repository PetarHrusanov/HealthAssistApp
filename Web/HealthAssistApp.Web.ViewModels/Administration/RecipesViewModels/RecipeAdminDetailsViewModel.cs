// <copyright file="RecipeAdminDetailsViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.RecipesViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Text.RegularExpressions;
    using Ganss.XSS;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class RecipeAdminDetailsViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Instructions")]
        public string InstructionForPreparation { get; set; }

        [DisplayName("Instructions")]
        public string ShortInstructionForPreparation
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.InstructionForPreparation, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        [DisplayName("Instructions For Preparation")]
        public string SanitizedInstructionForPreparation
            => new HtmlSanitizer().Sanitize(this.InstructionForPreparation);

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        public bool Vegan { get; set; }

        public bool Vegetarian { get; set; }

        [DisplayName("Part of Meal")]
        public PartOfMeal PartOfMeal { get; set; }

        [DisplayName("Glycemic Index")]
        public GlycemicIndex GlycemicIndex { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public DateTime ModifiedOn { get; set; }

        public int Calories { get; set; }
    }
}
