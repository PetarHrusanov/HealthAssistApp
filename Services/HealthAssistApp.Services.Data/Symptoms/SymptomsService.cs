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
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Services.Mapping;

    public class SymptomsService : ISymptomsServices
    {
        private readonly IRepository<UserSymptoms> userSymptomsRepository;
        private readonly IRepository<Symptom> symptomsRepository;

        public SymptomsService(IRepository<UserSymptoms> userSymptomsRepository, IRepository<Symptom> symptomsRepository)
        {
            this.userSymptomsRepository = userSymptomsRepository;
            this.symptomsRepository = symptomsRepository;
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

        public IEnumerable<T> SymptomsDropDownMenu<T>()
        {
            IQueryable<Symptom> query =
                this.symptomsRepository.All();

            return query.To<T>().ToList();
        }
    }
}
