// <copyright file="UserSymptoms.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models.DiseaseModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HealthAssistApp.Data.Common.Models;

    public class UserSymptoms : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MaxLength(300)]
        public string SystemName { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
