// <copyright file="HealthDosiersService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;

    public class HealthDosiersService : IHealthDosiersService
    {
        private readonly IRepository<HealthDosier> healthDosierRepository;

        public HealthDosiersService(IRepository<HealthDosier> healthDosierRepository)
        {
            this.healthDosierRepository = healthDosierRepository;
        }

        public Task<string> CreateHealthDosierAsync(int diseaseId, string healthDosierId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateHealthDosierAsync(
            int healthParametersId,
            int foodRegimenId,
            int workoutProgramId,
            bool smoker,
            bool drinkAlcohol,
            int allergiesId,
            string userId)
        {
            var healthDosier = new HealthDosier
            {
                HealthParametersId = healthParametersId,
                FoodRegimenId = foodRegimenId,
                WorkoutProgramId = workoutProgramId,
                Smoker = smoker,
                DrinkAlcohol = drinkAlcohol,
                AllergiesId = allergiesId,
                ApplicationUserId = userId,
            };
            await this.healthDosierRepository.AddAsync(healthDosier);
            await this.healthDosierRepository.SaveChangesAsync();
            return healthDosier.Id;
        }

        public HealthDosier GetById(string userId)
        {
            var healthDosier = this.healthDosierRepository.All().Where(x => x.ApplicationUserId == userId).FirstOrDefault();
            return healthDosier;
        }
    }
}
