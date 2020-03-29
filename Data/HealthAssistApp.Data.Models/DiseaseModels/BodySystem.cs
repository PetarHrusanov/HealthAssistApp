using System.Collections.Generic;
using HealthAssistApp.Data.Common.Models;

namespace HealthAssistApp.Data.Models
{
    public class BodySystem: BaseModel<int>
    {
        public BodySystem()
        {
            this.Symptoms = new HashSet<Symptom>();
        }

        public string Name { get; set; }

        public virtual ICollection<Symptom> Symptoms { get; set; }
    }
}
