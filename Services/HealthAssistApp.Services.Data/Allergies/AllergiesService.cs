// <copyright file="AllergiesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class AllergiesService : IAllergiesService
    {
        private readonly IRepository<Allergies> allergiesRepository;
        private readonly IRepository<HealthDosier> healthDosierRepository;

        public AllergiesService(IRepository<Allergies> allergiesRepository, IRepository<HealthDosier> healthDosierRepository)
        {
            this.allergiesRepository = allergiesRepository;
            this.healthDosierRepository = healthDosierRepository;
        }

        public async Task<int> CreateAsync(
            bool milk,
            bool eggs,
            bool fish,
            bool crustacean,
            bool treenuts,
            bool peanuts,
            bool wheat,
            bool soybeans,
            string userId)
        {
            var allergies = new Allergies
            {
                Milk = milk,
                Eggs = eggs,
                Fish = fish,
                Crustacean = crustacean,
                TreeNuts = treenuts,
                Peanuts = peanuts,
                Wheat = wheat,
                Soybeans = soybeans,
                ApplicationUserId = userId,
            };

            await this.allergiesRepository.AddAsync(allergies);
            await this.allergiesRepository.SaveChangesAsync();
            return allergies.Id;
        }

        public async Task<Allergies> GetByUserIdAsync(string userId)
        {
            var allergies = await this.allergiesRepository
                .All()
                .Where(a => a.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            return allergies;
        }

        public async Task<int> ModifyAsync(
            bool milk,
            bool eggs,
            bool fish,
            bool crustacean,
            bool treenuts,
            bool peanuts,
            bool wheat,
            bool soybeans,
            string userId)
        {
            var allergy = await this.allergiesRepository.All()
                .Where(x => x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();
            allergy.Milk = milk;
            allergy.Eggs = eggs;
            allergy.Fish = fish;
            allergy.Crustacean = crustacean;
            allergy.TreeNuts = treenuts;
            allergy.Peanuts = peanuts;
            allergy.Wheat = wheat;
            allergy.Soybeans = soybeans;

            this.allergiesRepository.Update(allergy);
            await this.allergiesRepository.SaveChangesAsync();

            return allergy.Id;
        }

        public async Task DeleteByUserIdAsync(string id)
        {
            var allergies = await this.allergiesRepository
                .All()
                .FirstOrDefaultAsync(h => h.ApplicationUserId == id);

            this.allergiesRepository.Delete(allergies);
            await this.allergiesRepository.SaveChangesAsync();
        }

        public async Task<T> ViewByUserIdAsync<T>(string userId)
        {
            var allergy = await this.allergiesRepository
                .AllAsNoTracking()
                .Where(x => x.ApplicationUserId == userId)
                .To<T>()
                .FirstOrDefaultAsync();
            return allergy;
        }
    }
}
