// <copyright file="SymptomsInputViewModel.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.ViewModels.Administration.SymptomsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class SymptomsInputViewModel
    {
        [DisplayName("Symptom")]
        public int SymptomId { get; set; }

        public string Description { get; set; }

        [DisplayName("Body System Id")]
        public int BodySystemId { get; set; }

        public IEnumerable<BodySystemsDropDownViewModel> bodySystems { get; set; }

    }
}
