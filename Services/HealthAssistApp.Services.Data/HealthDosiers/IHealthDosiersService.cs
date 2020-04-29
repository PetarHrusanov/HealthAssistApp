// <copyright file="IHealthDosiersService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;

    public interface IHealthDosiersService
    {
        public Task<string> CreateHealthDosierAsync(
            int healthParametersId,
            int foodRegimenId,
            int workoutProgramId,
            bool smoker,
            bool drinkAlcohol,
            int allergiesId,
            string userId);

        Task<T> GetViewByIdAsync<T>(string id);

        Task<T> GetViewByUserIdAsync<T>(string userId);

        Task<string> GetIdByUserId(string userId);

        Task<HealthDosier> GetByUserIdAsync(string userId);

        Task UserSideDeleteAsync(string id);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
