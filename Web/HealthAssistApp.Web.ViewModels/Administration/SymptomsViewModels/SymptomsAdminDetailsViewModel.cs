// <copyright file="SymptomsAdminDetailsViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.SymptomsViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class SymptomsAdminDetailsViewModel : IMapFrom<Symptom>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public int BodySystemId { get; set; }

        public string BodySystemName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
