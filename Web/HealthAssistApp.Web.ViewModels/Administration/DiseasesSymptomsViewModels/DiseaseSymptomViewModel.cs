// <copyright file="DiseaseSymptomViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System.ComponentModel;

namespace HealthAssistApp.Web.ViewModels.Administration.DiseasesSymptomsViewModels
{
    public class DiseaseSymptomViewModel
    {
        [DisplayName("Disease Id")]
        public int DiseaseId { get; set; }

        [DisplayName("Disease Name")]
        public string DiseaseName { get; set; }

        [DisplayName("Symptom Id")]
        public int SymptomId { get; set; }

        [DisplayName("Symptom Name")]
        public string SymptomName { get; set; }

        public string IdS { get; set; }
    }
}
