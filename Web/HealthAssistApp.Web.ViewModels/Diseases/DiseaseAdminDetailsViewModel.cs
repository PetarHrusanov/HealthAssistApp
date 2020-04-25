﻿// <copyright file="DiseaseAdminDetailsViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    using System;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class DiseaseAdminDetailsViewModel : IMapFrom<Disease>
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
