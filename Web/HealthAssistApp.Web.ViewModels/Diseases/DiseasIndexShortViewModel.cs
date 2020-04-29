// <copyright file="DiseaseViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text.RegularExpressions;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class DiseasIndexShortViewModel : IMapFrom<Disease>
    {
        public DiseasIndexShortViewModel()
        {
            this.DiseaseSymptoms = new HashSet<DiseaseSymptom>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        public string Advice { get; set; }

        public GlycemicIndex? GlycemicIndex { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
