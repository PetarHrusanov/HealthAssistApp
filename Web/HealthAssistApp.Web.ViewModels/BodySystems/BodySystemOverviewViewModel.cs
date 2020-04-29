// <copyright file="BodySystemOverviewViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.BodySystems
{
    using System;
    using System.ComponentModel;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class BodySystemOverviewViewModel : IMapFrom<BodySystem>
    {
        public int Id { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public DateTime ModifiedOn { get; set; }

        public string Name { get; set; }
    }
}
