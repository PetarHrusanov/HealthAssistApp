using System.Collections.Generic;
using HealthAssistApp.Data.Common.Models;

namespace HealthAssistApp.Data.Models
{
    public class Symptom: BaseModel<int>
    {

        public Symptom()
        {
            this.DiseaseSymptoms = new HashSet<DiseaseSymptom>();
        }

        public string Description { get; set; }

        public BodySystem BodySystem { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }

    }
}