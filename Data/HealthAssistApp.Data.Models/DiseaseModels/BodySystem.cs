// <copyright file="BodySystem.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HealthAssistApp.Data.Common.Models;

    public class BodySystem: BaseModel<int>
    {
        public BodySystem()
        {
            this.Symptoms = new HashSet<Symptom>();
        }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public virtual ICollection<Symptom> Symptoms { get; set; }
    }
}
