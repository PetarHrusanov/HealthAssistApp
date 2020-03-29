namespace HealthAssistApp.Data.Models.DiseaseModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HealthDosierDisease
    {
        // Health Dosier
        [Required]
        public string HealthDosierId { get; set; }

        public virtual HealthDosier HealthDosier { get; set; }

        // Disease
        [Required]
        public int DiseaseId { get; set; }

        public virtual Disease Disease { get; set; }
    }
}
