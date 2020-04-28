namespace HealthAssistApp.Web.ViewModels.Diseases
{
    using System;
    using System.Collections.Generic;

    public class IndexDiseasesViewModel
    {
        public IEnumerable<DiseaseViewModel> Diseases { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
