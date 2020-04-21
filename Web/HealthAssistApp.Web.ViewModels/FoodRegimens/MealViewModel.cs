// <copyright file="MealViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using HealthAssistApp.Data.Models;
using HealthAssistApp.Services.Mapping;
using HealthAssistApp.Web.ViewModels.Recipes;

namespace HealthAssistApp.Web.ViewModels.FoodRegimens
{
    public class MealViewModel : IMapFrom<Meal>
    {
        public int BraekfastId { get; set; }

        public RecipeViewModel Breakfast { get; set; }

        public int LunchId { get; set; }

        public RecipeViewModel Lunch { get; set; }

        public int DinerId { get; set; }

        public RecipeViewModel Diner { get; set; }
    }
}
