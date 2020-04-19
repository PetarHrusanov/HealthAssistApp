// <copyright file="HealthDosierOverview.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.HealthDosier
{
    using System.Collections.Generic;

    using HealthAssistApp.Web.ViewModels.Allergies;
    using HealthAssistApp.Web.ViewModels.Diseases;
    using HealthAssistApp.Web.ViewModels.Enums;
    using HealthAssistApp.Web.ViewModels.HealthParameters;
    using HealthAssistApp.Data.Models;

    public class HealthDosierOverview
    {
        public HealthDosierOverview()
        {
            this.Diseases = new HashSet<DiseaseViewModel>();
        }

        public HealthParametersViewModel HealthParameters { get; set; }

        public string UserId { get; set; }

        public int FoodRegimenId { get; set; }

        public int WorkingOutProgramId { get; set; }

        public bool Smoker { get; set; }

        public bool DrinkAlcohol { get; set; }

        public int AllergiesId { get; set; }

        public NutritionalStatus NutritionalStatus { get; set; }

        public IEnumerable<DiseaseViewModel> Diseases { get; set; }
    }
}
