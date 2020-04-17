// <copyright file="DiseaseSymptom.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration
{
    public class DiseaseSymptomViewModel
    {
        // Disease
        public int DiseaseId { get; set; }

        public string DiseaseName { get; set; }

        // Symptom
        public int SymptomId { get; set; }

        public string SymptomName { get; set; }

        public string IdS { get; set; }
    }
}
