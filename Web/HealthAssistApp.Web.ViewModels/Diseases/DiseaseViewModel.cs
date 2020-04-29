// <copyright file="DiseaseViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Ganss.XSS;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class DiseaseViewModel : IMapFrom<Disease>
    {
        public DiseaseViewModel()
        {
            this.DiseaseSymptoms = new HashSet<DiseaseSymptom>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Description")]
        public string SanitizedDescriptions
            => new HtmlSanitizer().Sanitize(this.Description);

        public string Advice { get; set; }

        [DisplayName("Advice")]
        public string SanitizedAdvice
            => new HtmlSanitizer().Sanitize(this.Advice);

        [DisplayName("Glycemic Index")]
        public GlycemicIndex? GlycemicIndex { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
