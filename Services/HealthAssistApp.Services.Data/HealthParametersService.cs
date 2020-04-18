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
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class HealthParametersService : IHealthParametersService
    {
        private readonly IRepository<HealthParameters> healthParametersRepository;

        public HealthParametersService(IRepository<HealthParameters> healthParametersRepository)
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

        public HealthParameters GetByUserId(string userId)
        {
            var healthParameters = this.healthParametersRepository
                .All()
                .Where(a => a.ApplicationUserId == userId)
                .FirstOrDefault();

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

        public T ViewByUserId<T>(string userId)
        {
            var healthParameters = this.healthParametersRepository.All().Where(x => x.ApplicationUserId == userId)
                .To<T>().FirstOrDefault();
            return healthParameters;
        }
    }
}
