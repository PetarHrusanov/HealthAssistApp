// <copyright file="DiseaseSymptomViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System.ComponentModel;
using HealthAssistApp.Data.Models;
using HealthAssistApp.Services.Mapping;

namespace HealthAssistApp.Web.ViewModels.Administration.DiseasesSymptomsViewModels
{
    public class DiseaseSymptomViewModel : IMapFrom<DiseaseSymptom>
    {
        [DisplayName("Disease Id")]
        public int DiseaseId { get; set; }

        [DisplayName("Disease Name")]
        public string DiseaseName { get; set; }

        [DisplayName("Symptom Id")]
        public int SymptomId { get; set; }

        [DisplayName("Symptom Name")]
        public string SymptomDescription { get; set; }

        public string IdS
        {
            get
            {
                var ids = $"{DiseaseId}X{SymptomId}";
                return ids;
            }
        }
    }
}
