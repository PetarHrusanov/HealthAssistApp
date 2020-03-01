﻿namespace HealthAssistApp.Data.Models
{
    using System.Collections.Generic;
    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models.Enums;

    public class Recipe: BaseModel<int>
    {

        public Recipe()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredients>();
        }

        public string Name { get; set; }

        public string InstructionForPreparation { get; set; }

        public bool Vegan { get; set; }

        public bool Vegetarian { get; set; }

        public GlycemicIndex GlycemicIndex { get; set; }

        public int Calories { get; set; }

        public ICollection<RecipeIngredients> RecipeIngredients { get; set; }

    }
}