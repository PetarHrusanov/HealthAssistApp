// <copyright file="FoodRegimenMealsIndex.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.FoodRegimens
{
    using System;
    using System.Collections.Generic;

    public class FoodRegimenMealsIndex
    {
        public ICollection<MealViewModel> Meals { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public string HealthDosierId { get; set; }
    }
}
