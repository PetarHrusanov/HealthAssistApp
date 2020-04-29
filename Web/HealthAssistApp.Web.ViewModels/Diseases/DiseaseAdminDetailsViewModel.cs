// <copyright file="DiseaseAdminDetailsViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Text.RegularExpressions;
    using Ganss.XSS;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class DiseaseAdminDetailsViewModel : IMapFrom<Disease>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        [DisplayName("Description")]
        public string SanitizedDescriptions
            => new HtmlSanitizer().Sanitize(this.Description);

        public string Advice { get; set; }

        public string ShortAdvice
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Advice, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        [DisplayName("Advice")]
        public string SanitizedAdvice
           => new HtmlSanitizer().Sanitize(this.SanitizedAdvice);

        [DisplayName("Glycemic Index")]
        public GlycemicIndex? GlycemicIndex { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [DisplayName("Deleted On")]
        public DateTime? DeletedOn { get; set; }
    }
}
