﻿namespace HealthAssistApp.Data.Models.DiseaseModels
{
    using System;

    using HealthAssistApp.Data.Common.Models;

    public class UserSymptoms : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public string SystemName { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
