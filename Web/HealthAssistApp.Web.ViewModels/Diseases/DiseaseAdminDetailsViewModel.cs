// <copyright file="DiseaseAdminDetailsViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System;
using HealthAssistApp.Data.Models.Enums;

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    public class DiseaseAdminDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Advice { get; set; }

        public GlycemicIndex? GlycemicIndex { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
