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

        IEnumerable<T> GetAllPaginatedAsync<T>(int? take = null, int skip = 0);

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        Task<int> GetDiseasesCountAsync();

        IEnumerable<T> GetByHealthDosier<T>(string healthDosierId);

        Task<int> ModifyDiseaseAsync(
            int id,
            string name,
            string description,
            string advice,
            bool isDeleted);

        Task DeleteByIdAsync(int id);

        Task<IEnumerable<T>> DiseasesDropDownMenuAsync<T>();

        Task<string> CreateHealthDosierDiseaseAsync(int diseaseId, string healthDosierId);

        Task DeleteHealthDosierDiseasesByHealthIdAsync(string id);

        Task CreateDiseaseSymptomAsync(int diseaseId, int symptomId);

        Task DeleteDiseaseSymptomAsync(string idS);

        Task<T> GetDiseaseSymptomAsync<T>(string idS);

        Task<IEnumerable<T>> GetAllDiseaseSymptomsAsync<T>();
    }
}
