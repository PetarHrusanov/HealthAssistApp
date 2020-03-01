namespace HealthAssistApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DiseaseSymptom
    {
        [Required]
        public int DiseaseId { get; set; }
        public virtual Disease Disease { get; set; }

        [Required]
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}