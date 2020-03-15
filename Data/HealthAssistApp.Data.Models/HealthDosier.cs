namespace HealthAssistApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Data.Models.WorkingOut;

    public class HealthDosier: BaseDeletableModel<string>
    {

        public HealthDosier()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int HealthParametersId { get; set; }
        public HealthParameters HealthParameters { get; set; }

        public int FoodRegimenId { get; set; }
        public virtual FoodRegimen FoodRegimen { get; set; }

        [ForeignKey("WorkoutProgram")]
        public int WorkoutProgramId { get; set; }
        public virtual WorkoutProgram WorkoutProgram { get; set; }

        public bool Smoker { get; set; }

        public ICollection<Disease> Diseases { get; set; }

        //da go pomislq
        public int AllergiesId { get; set; }
        public Allergies Allergies { get; set; }

        //da obmislq predimstwa i nedostataci na tazi vrazka
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        //To Do Risk From Diseases

    }
}
