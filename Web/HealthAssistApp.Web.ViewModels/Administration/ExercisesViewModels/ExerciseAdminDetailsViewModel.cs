// <copyright file="ExerciseAdminMofidyViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.ExercisesViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Text.RegularExpressions;
    using Ganss.XSS;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Mapping;

    public class ExerciseAdminDetailsViewModel : IMapFrom<Exercise>, IMapTo<Exercise>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Instructions { get; set; }

        [DisplayName("Instructions")]
        public string SanitizedInstructions
           => new HtmlSanitizer().Sanitize(this.Instructions);

        public string ShortInstructions
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Instructions, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        [Required]
        public ExerciseComplexity ExerciseComplexity { get; set; }
    }
}
