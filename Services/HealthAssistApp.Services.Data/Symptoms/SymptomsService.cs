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
    using Microsoft.EntityFrameworkCore;

    public class SymptomsService : ISymptomsServices
    {
        private readonly IRepository<UserSymptoms> userSymptomsRepository;
        private readonly IRepository<Symptom> symptomsRepository;

        public SymptomsService(IRepository<UserSymptoms> userSymptomsRepository, IRepository<Symptom> symptomsRepository)
        {
            this.userSymptomsRepository = userSymptomsRepository;
            this.symptomsRepository = symptomsRepository;
        }

        public async Task<int> CreateSymptomAsync(string description, int bodySystemId)
        {
            var symptom = new Symptom
            {
                Description = description,
                BodySystemId = bodySystemId,
            };

            await this.symptomsRepository.AddAsync(symptom);
            await this.symptomsRepository.SaveChangesAsync();

            return symptom.Id;
        }

        public async Task<T> GetModelByIdAsync<T>(int id)
        {
            var symptom = await this.symptomsRepository
                .All()
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return symptom;
        }

        public async Task<int> ModifySymptomAsync(int symptomId, string description, int bodySystemId)
        {
            var symptom = await this.symptomsRepository
                .All()
                .Where(s => s.Id == symptomId)
                .FirstOrDefaultAsync();

            symptom.Description = description;
            symptom.BodySystemId = bodySystemId;

            this.symptomsRepository.Update(symptom);
            await this.symptomsRepository.SaveChangesAsync();

            return symptom.Id;
        }

        public async Task DeleteSymptomAsync(int id)
        {
            var symptom = await this.symptomsRepository
                .All()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            this.symptomsRepository.Delete(symptom);
            await this.symptomsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllinViewModelAsync<T>()
        {
            var symptoms = await this.symptomsRepository
                .All()
                .To<T>()
                .ToListAsync();

            return symptoms;
        }

        public IEnumerable<T> SymptomsDropDownMenu<T>()
        {
            IQueryable<Symptom> query =
                this.symptomsRepository.All();

            return query.To<T>().ToList();
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

        public async Task DeleteUserSymptomsAsync(string id)
        {
            var userSymptoms = await this.userSymptomsRepository
                .All()
                .Where(u => u.ApplicationUserId == id)
                .ToListAsync();

            foreach (var item in userSymptoms)
            {
                this.userSymptomsRepository.Delete(item);
                await this.userSymptomsRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<string>> GetSystemNameFromUserId(string userId)
        {
            var systemNames = this.userSymptomsRepository
                .All()
                .Where(u => u.ApplicationUserId == userId)
                .Select(d => d.SystemName);

            return systemNames;
        }
    }
}
