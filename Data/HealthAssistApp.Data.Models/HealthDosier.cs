﻿namespace HealthAssistApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.FoodModels;

    public class HealthDosier: BaseDeletableModel<string>
    {

        public HealthDosier()
        {
            this.Id = Guid.NewGuid().ToString();
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

        //da obmislq predimstwa i nedostataci na tazi vrazka
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        //To Do Risk From Diseases

    }
}
