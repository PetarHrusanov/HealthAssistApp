// <copyright file="HealthDosierAdminOverviewViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.HealthDosierViewModels
{
    using System;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class HealthDosierAdminOverviewViewModel : IMapFrom<HealthDosier>
    {
        public int HealthParametersId { get; set; }

        public string Id { get; set; }

        public int FoodRegimenId { get; set; }

        public int WorkOutProgramId { get; set; }

        public bool Smoker { get; set; }

        public bool DrinkAlcohol { get; set; }

        public int AllergiesId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserUsername { get; set; }

    }
}
