using System.Collections.Generic;
using HealthAssistApp.Data.Common.Models;
using HealthAssistApp.Data.Models.Enums;

namespace HealthAssistApp.Data.Models
{
    public class Disease: BaseDeletableModel<int>
    {

        public Disease()
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