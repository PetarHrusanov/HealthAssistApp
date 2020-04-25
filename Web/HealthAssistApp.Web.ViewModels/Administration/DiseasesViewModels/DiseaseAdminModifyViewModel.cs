// <copyright file="DiseaseAdminModifyViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.DiseasesViewModel
{
    using System.ComponentModel.DataAnnotations;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class DiseaseAdminModifyViewModel : IMapFrom<Disease>, IMapTo<Disease>
    {
        public int Id { get; set; }

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

        public bool IsDeleted { get; set; }
    }
}
