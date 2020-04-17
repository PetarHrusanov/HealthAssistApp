// <copyright file="SymptomsIndexViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.SymptomsViewModels
{
    public class SymptomsIndexViewModel
    {
        public int SymptomId { get; set; }

        public string Description { get; set; }

        public int BodySystemId { get; set; }

        public string BodySystemName { get; set; }
    }
}
