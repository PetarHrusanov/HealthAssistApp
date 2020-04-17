using System;
using System.Collections.Generic;

namespace HealthAssistApp.Web.ViewModels.Administration
{
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
