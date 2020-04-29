// <copyright file="AdminTextFilesModifyViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.AdminitrationTextFilesViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class AdminTextFilesModifyViewModel : IMapFrom<TextFilesForAdministration>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
