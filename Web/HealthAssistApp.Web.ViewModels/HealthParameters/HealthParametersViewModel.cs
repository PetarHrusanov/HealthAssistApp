// <copyright file="HealthParametersViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System.ComponentModel;
using HealthAssistApp.Services.Mapping;

namespace HealthAssistApp.Web.ViewModels.HealthParameters
{
    public class HealthParametersViewModel : IMapFrom<HealthAssistApp.Data.Models.HealthParameters>
    {
        public int Age { get; set; }

        public int Weight { get; set; }

        public decimal Height { get; set; }

        [DisplayName("Body Mass Index")]
        public decimal BodyMassIndex { get; set; }

        [DisplayName("Required Water per Day in Liters")]
        public decimal WaterPerDay { get; set; }
    }
}
