// <copyright file="Disease.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Data.Models.Enums;

    public class Disease: BaseDeletableModel<int>
    {

        public Disease()
        {
            this.DiseaseSymptoms = new HashSet<DiseaseSymptom>();
            this.HealthDosierDiseases = new HashSet<HealthDosierDisease>();
        }

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

        public virtual ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }

        public virtual ICollection<HealthDosierDisease> HealthDosierDiseases { get; set; }

    }
}