// <copyright file="DiseaseSymptomInputViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;

    public class DiseaseSymptomInputViewModel
    {
        // Disease
        public int DiseaseId { get; set; }

        public IEnumerable<DiseasesDropDownViewModel> diseases { get; set; }

        // Symptom
        public int SymptomId { get; set; }

        public IEnumerable<SymptomsDropDownViewModel> symptoms { get; set; }
    }
}
