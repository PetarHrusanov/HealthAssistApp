// <copyright file="SymptomsDropDownViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using HealthAssistApp.Data.Models;
using HealthAssistApp.Services.Mapping;

namespace HealthAssistApp.Web.ViewModels.Administration
{
    public class SymptomsDropDownViewModel : IMapFrom<Symptom>
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
