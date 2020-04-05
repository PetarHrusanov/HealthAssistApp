// <copyright file="DiseaseViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    using System;
    using System.Collections.Generic;

    using HealthAssistApp.Data.Models;
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

        public string Advice { get; set; }

        public GlycemicIndex? GlycemicIndex { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
