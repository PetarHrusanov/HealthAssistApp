using System.Collections.Generic;
using HealthAssistApp.Data.Common.Models;
using HealthAssistApp.Data.Models.DiseaseModels;
using HealthAssistApp.Data.Models.Enums;

namespace HealthAssistApp.Data.Models
{
    public class Disease: BaseDeletableModel<int>
    {

        public Disease()
        {
            this.DiseaseSymptoms = new HashSet<DiseaseSymptom>();
            this.HealthDosierDiseases = new HashSet<HealthDosierDisease>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Advice { get; set; }

        public GlycemicIndex? GlycemicIndex { get; set; }

        public virtual ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }

        public virtual ICollection<HealthDosierDisease> HealthDosierDiseases { get; set; }

    }
}