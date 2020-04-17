// <copyright file="AllergiesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Linq;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Services.Mapping;

    public class AllergiesService : IAllergiesService
    {
        private readonly IRepository<Allergies> allergiesRepository;
        private readonly IRepository<HealthDosier> healthDosierRepository;

        public AllergiesService(IRepository<Allergies> allergiesRepository, IRepository<HealthDosier> healthDosierRepository)
        {
            this.allergiesRepository = allergiesRepository;
            this.healthDosierRepository = healthDosierRepository;
        }

        public Allergies GetByUserId (string userId)
        {
            var allergies = this.healthDosierRepository
                .All()
                .Where(a => a.ApplicationUserId == userId)
                .Select(a => a.Allergies)
                .FirstOrDefault();

            return allergies;
        }

        public T ViewByUserId<T>(string userId)
        {
            var allergy = this.allergiesRepository.All().Where(x => x.ApplicationUserId == userId)
                .To<T>().FirstOrDefault();
            return allergy;
        }
    }
}
