// <copyright file="HealthDosierAdminOverviewViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.HealthDosierViewModels
{
    using System;
    using System.ComponentModel;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class HealthDosierAdminOverviewViewModel : IMapFrom<HealthDosier>
    {
        public string Id { get; set; }

        [DisplayName("Health Parameters")]
        public int HealthParametersId { get; set; }

        [DisplayName("Food Regimen")]
        public int FoodRegimenId { get; set; }

        [DisplayName("Workouts Id")]
        public int WorkOutProgramId { get; set; }

        public bool Smoker { get; set; }

        [DisplayName("Drinks Alcohol")]
        public bool DrinkAlcohol { get; set; }

        [DisplayName("Allergies Id")]
        public int AllergiesId { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [DisplayName("Deleted On")]
        public DateTime DeletedOn { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public DateTime ModifiedOn { get; set; }

        [DisplayName("User Id")]
        public string ApplicationUserId { get; set; }

        [DisplayName("Username")]
        public string ApplicationUserUsername { get; set; }

    }
}
