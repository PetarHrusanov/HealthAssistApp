// <copyright file="SymptomsIndexViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using HealthAssistApp.Data.Models;
using HealthAssistApp.Services.Mapping;

namespace HealthAssistApp.Web.ViewModels.Administration.SymptomsViewModels
{
    public class SymptomsIndexViewModel : IMapFrom<Symptom>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int BodySystemId { get; set; }

        public string BodySystemName { get; set; }
    }
}
