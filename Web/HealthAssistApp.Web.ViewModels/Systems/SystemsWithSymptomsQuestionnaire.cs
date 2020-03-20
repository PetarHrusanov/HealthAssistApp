using System;
using System.Collections.Generic;
using HealthAssistApp.Web.ViewModels.Systems;

namespace HealthAssistApp.Web.ViewModels.Systems
{
    public class SystemsWithSymptomsQuestionnaire
    {
        //public SystemsWithSymptomsQuestionnaire()
        //{
        //    this.Symptoms = new HashSet<SymptomsForSystems>();
        //}
        public string Name { get; set; }

        public IEnumerable<SymptomsForSystems> Symptoms { get; set; }
    }
}
