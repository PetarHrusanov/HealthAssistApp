// <copyright file="HealthParametersViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using HealthAssistApp.Services.Mapping;

namespace HealthAssistApp.Web.ViewModels.HealthParameters
{
    public class HealthParametersViewModel : IMapFrom<HealthAssistApp.Data.Models.HealthParameters>
    {
        public int Age { get; set; }

        public int Weight { get; set; }

        public decimal Height { get; set; }

        public decimal BodyMassIndex { get; set; }

        public decimal WaterPerDay { get; set; }
    }
}
