// <copyright file="BodySystemsInputViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.BodySystems
{
    using System.ComponentModel.DataAnnotations;

    public class BodySystemsInputViewModel
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
