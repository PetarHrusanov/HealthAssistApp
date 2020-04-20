// <copyright file="SymptomsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models.DiseaseModels;

    public class SymptomsService : ISymptomsServices
    {
        private readonly IRepository<UserSymptoms> userSymptomsRepository;
        //private readonly IRepository<> userSymptomsRepository;

        public SymptomsService(IRepository<UserSymptoms> userSymptomsRepository)
        {
            this.userSymptomsRepository = userSymptomsRepository;
        }

        public async Task<int> CreateUserSymptomAsync(string description, string systemName, string userId)
        {
            var userSymptom = new UserSymptoms
            {
                Description = description,
                SystemName = systemName,
                ApplicationUserId = userId,
            };

            await this.userSymptomsRepository.AddAsync(userSymptom);
            await this.userSymptomsRepository.SaveChangesAsync();
            return userSymptom.Id;
        }

        public async Task<IEnumerable<string>> GetSystemNameFromUserId(string userId)
        {
            var systemNames = this.userSymptomsRepository
                .All().Where(u => u.ApplicationUserId == userId)
                .Select(d => d.SystemName);

            return systemNames;
        }
    }
}
