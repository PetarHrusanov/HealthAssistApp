// <copyright file="IHealthDosiersService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;

    public interface IHealthDosiersService
    {
        //IEnumerable<T> GetAll<T>(int? count = null);

        public Task<string> CreateHealthDosierAsync(
            int healthParametersId,
            int foodRegimenId,
            int workoutProgramId,
            bool smoker,
            bool drinkAlcohol,
            int allergiesId,
            string userId);

        Task<T> GetByIdAsync<T>(string userId);

        Task<string> GetIdByUserId(string userId);

        Task<HealthDosier> GetByUserIdAsync(string userId);

        //IEnumerable<T> GetByHealthDosier<T>(string healthDosierId);

        //public Task<string> CreateHealthDosierDiseaseAsync(int diseaseId, string healthDosierId);
    }
}
