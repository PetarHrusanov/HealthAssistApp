// <copyright file="DiseasesDropDownViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using HealthAssistApp.Data.Models;
using HealthAssistApp.Services.Mapping;

namespace HealthAssistApp.Web.ViewModels.Administration
{
    public class DiseasesDropDownViewModel : IMapFrom<Disease>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
