// <copyright file="Symptom.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HealthAssistApp.Data.Common.Models;

    public class Symptom: BaseModel<int>
    {
        public Symptom()
        {
            this.DiseaseSymptoms = new HashSet<DiseaseSymptom>();
        }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [ForeignKey("BodySystem")]
        public int BodySystemId { get; set; }

        public BodySystem BodySystem { get; set; }

        public virtual ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }

    }
}