// <copyright file="HealthParametersService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class HealthParametersService : IHealthParametersService
    {
        private readonly IDeletableEntityRepository<HealthParameters> healthParametersRepository;

        public HealthParametersService(IDeletableEntityRepository<HealthParameters> healthParametersRepository)
        {
            this.healthParametersRepository = healthParametersRepository;
        }

        public async Task<int> CreateAsync(
            int age,
            int weight,
            decimal height,
            string userId)
        {
            var healthParameters = new HealthParameters
            {
                Age = age,
                Weight = weight,
                Height = height,
                BodyMassIndex = weight / (height * height),
                WaterPerDay = weight * 0.033m,
                ApplicationUserId = userId,
            };

            await this.healthParametersRepository.AddAsync(healthParameters);
            await this.healthParametersRepository.SaveChangesAsync();
            return healthParameters.Id;
        }

        public async Task<HealthParameters> GetByUserIdAsync(string userId)
        {
            var healthParameters = await this.healthParametersRepository
                .All()
                .Where(a => a.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            return healthParameters;
        }

        public async Task<int> ModifyAsync(
            int age,
            int weight,
            decimal height,
            string userId)
        {
            var healthParameters = await this.healthParametersRepository.All()
                .Where(x => x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            healthParameters.Age = age;
            healthParameters.Weight = weight;
            healthParameters.Height = height;
            healthParameters.BodyMassIndex = weight / (height * height);
            healthParameters.WaterPerDay = weight * 0.033m;

            this.healthParametersRepository.Update(healthParameters);
            await this.healthParametersRepository.SaveChangesAsync();

            return healthParameters.Id;
        }

        public async Task UserSideDeleteUserIdAsync(string userId)
        {
            var healthParam = await this.healthParametersRepository
                .All()
                .FirstOrDefaultAsync(h => h.ApplicationUserId == userId);

            this.healthParametersRepository.Delete(healthParam);
            await this.healthParametersRepository.SaveChangesAsync();
        }

        public T ViewByUserId<T>(string userId)
        {
            var healthParameters = this.healthParametersRepository
                .All()
                .Where(x => x.ApplicationUserId == userId)
                .To<T>()
                .FirstOrDefault();
            return healthParameters;
        }

        public NutritionalStatus NutritionalStatusByBodyMassIndex(decimal bodyMassIndex)
        {
            NutritionalStatus nutritionalStatus = NutritionalStatus.Normal;

            if (bodyMassIndex < 18.5m)
            {
                nutritionalStatus = NutritionalStatus.Underweight;
            }
            else if (bodyMassIndex <= 24.9m)
            {
                nutritionalStatus = NutritionalStatus.Normal;
            }
            else if (bodyMassIndex <= 29.9m)
            {
                nutritionalStatus = NutritionalStatus.Preobesity;
            }
            else if (bodyMassIndex <= 34.9m)
            {
                nutritionalStatus = NutritionalStatus.ObesityI;
            }
            else if (bodyMassIndex <= 39.9m)
            {
                nutritionalStatus = NutritionalStatus.ObesityII;
            }
            else
            {
                nutritionalStatus = NutritionalStatus.ObesityIII;
            }

            return nutritionalStatus;
        }
    }
}
