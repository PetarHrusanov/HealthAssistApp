namespace HealthAssistApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Data.Models.WorkingOut;

    public class HealthDosier: BaseDeletableModel<string>
    {
        public HealthDosier()
        {
            this.Id = Guid.NewGuid().ToString();
            this.HealthDosierDiseases = new HashSet<HealthDosierDisease>();
        }

        public int HealthParametersId { get; set; }

        public HealthParameters HealthParameters { get; set; }

        // FoodRegimen
        [ForeignKey("FoodRegimen")]
        public int FoodRegimenId { get; set; }

        public FoodRegimen FoodRegimen { get; set; }

        // Working Out
        [ForeignKey("WorkoutProgram")]
        public int WorkoutProgramId { get; set; }

        public virtual WorkoutProgram WorkoutProgram { get; set; }

        // da go pomislq
        [ForeignKey("Allergies")]
        public int AllergiesId { get; set; }

        public Allergies Allergies { get; set; }

        // Health Dosier Specific
        public bool Smoker { get; set; }

        public bool DrinkAlcohol { get; set; }

        public virtual ICollection<HealthDosierDisease> HealthDosierDiseases { get; set; }

        // da obmislq predimstwa i nedostataci na tazi vrazka
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        // To Do Risk From Diseases
    }
}
