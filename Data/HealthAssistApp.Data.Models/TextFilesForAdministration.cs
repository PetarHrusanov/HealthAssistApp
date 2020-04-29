// <copyright file="TextFilesForAdministration.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HealthAssistApp.Data.Common.Models;

    public class TextFilesForAdministration : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
