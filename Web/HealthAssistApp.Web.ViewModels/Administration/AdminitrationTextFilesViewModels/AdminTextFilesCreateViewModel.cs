// <copyright file="AdminTextFilesCreateViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.AdminitrationTextFilesViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AdminTextFilesCreateViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
