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

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(int id);

        T GetByName<T>(string name);

        IEnumerable<T> GetByHealthDosier<T>(string healthDosierId);

        Task<int> ModifyDiseaseAsync(
            int id,
            string name,
            string description,
            string advice,
            bool isDeleted);

        Task DeleteByIdAsync(int id);

        IEnumerable<T> DiseasesDropDownMenu<T>();

        Task<string> CreateHealthDosierDiseaseAsync(int diseaseId, string healthDosierId);

        Task CreateDiseaseSymptomAsync(int diseaseId, int symptomId);

        Task DeleteDiseaseSymptomAsync(int diseaseId, int symptomId);
    }
}
