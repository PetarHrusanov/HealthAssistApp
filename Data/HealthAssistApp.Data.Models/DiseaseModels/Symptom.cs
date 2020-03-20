using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("BodySystem")]
        public int BodySystemId { get; set; }
        public virtual BodySystem BodySystem { get; set; }

        public virtual ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }

    }
}