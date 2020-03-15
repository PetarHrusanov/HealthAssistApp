namespace HealthAssistApp.Data.Models.DiseaseModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HealthDosierDisease
    {
        [Required]
        public string HealthDosierId { get; set; }
        public virtual HealthDosier HealthDosier { get; set; }

        [Required]
        public int DiseaseId { get; set; }
        public virtual Disease Disease {get;set;}
    }
}
