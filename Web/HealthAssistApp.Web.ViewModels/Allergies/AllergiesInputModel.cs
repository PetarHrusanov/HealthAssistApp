// <copyright file="AllergiesInputModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Allergies
{
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Services.Mapping;

    public class AllergiesInputModel : IMapFrom<Allergies>
    {
        public bool Milk { get; set; }

        public bool Eggs { get; set; }

        public bool Fish { get; set; }

        public bool Crustacean { get; set; }

        public bool TreeNuts { get; set; }

        public bool Peanuts { get; set; }

        public bool Wheat { get; set; }

        public bool Soybeans { get; set; }
    }
}
