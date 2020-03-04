namespace HealthAssistApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.FoodModels;

    public class HealthDosier: BaseDeletableModel<string>
    {

        public HealthDosier()
        {
            Exercises = new HashSet<Exercise>();
        }

        public HealthParameters HealthParameters { get; set; }

        public FoodRegimen FoodRegimen { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public bool Smoker { get; set; }

        public ICollection<Disease> Diseases { get; set; }

        //da go pomislq
        public int AllergiesId { get; set; }
        public Allergies Allergies { get; set; }


        //To Do Risk From Diseases

    }
}
