// <copyright file="BodySystemsInputViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace HealthAssistApp.Web.ViewModels.BodySystems
{
    public class BodySystemsInputViewModel
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
