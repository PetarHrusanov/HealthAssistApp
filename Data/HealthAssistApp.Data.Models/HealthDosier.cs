namespace HealthAssistApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;

    public class HealthDosier: BaseDeletableModel<string>
    {

        public HealthDosier()
        {
            Exercises = new HashSet<Exercise>();
        }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get;set;}

        public HealthParameters HealthParameters { get; set; }

        public FoodRegimen FoodRegimen { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public bool Smoker { get; set; }

        public ICollection<Disease> Diseases { get; set; }

        //To Do Risk From Diseases

    }
}
