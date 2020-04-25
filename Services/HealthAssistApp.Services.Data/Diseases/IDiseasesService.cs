// <copyright file="IDiseasesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HealthAssistApp.Data.Models.Enums;

    public interface IDiseasesService
    {
        public Task<int> CreateAsync(
            string name,
            string description,
            string advice,
            GlycemicIndex? index);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        IEnumerable<T> GetByHealthDosier<T>(string healthDosierId);

        IEnumerable<T> DiseasesDropDownMenu<T>();

        public Task<string> CreateHealthDosierDiseaseAsync(int diseaseId, string healthDosierId);

        Task CreateDiseaseSymptomAsync(int diseaseId, int symptomId);

        Task DeleteDiseaseSymptomAsync(int diseaseId, int symptomId);
    }
}
