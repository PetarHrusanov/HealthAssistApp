// <copyright file="Meal.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.FoodModels;

    public class Meal : BaseModel<int>
    {
        public int BreakfastId { get; set; }

        public virtual Recipe Breakfast { get; set; }

        public int LunchId { get; set; }

        public virtual Recipe Lunch { get; set; }

        public int DinerId { get; set; }

        public virtual Recipe Diner { get; set; }

        [ForeignKey("FoodRegimen")]
        public int FoodRegimenId { get; set; }

        public virtual FoodRegimen FoodRegimen { get; set; }
    }
}
