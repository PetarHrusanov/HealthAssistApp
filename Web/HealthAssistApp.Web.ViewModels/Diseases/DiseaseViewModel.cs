using System;
using System.Collections.Generic;
using HealthAssistApp.Data.Models;
using HealthAssistApp.Data.Models.Enums;
using HealthAssistApp.Services.Mapping;

namespace HealthAssistApp.Web.ViewModels.Diseases
{
    public class DiseaseViewModel :IMapFrom<Disease>
    {
        public DiseaseViewModel()
        {
            this.DiseaseSymptoms = new HashSet<DiseaseSymptom>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Advice { get; set; }

        public GlycemicIndex? GlycemicIndex { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
