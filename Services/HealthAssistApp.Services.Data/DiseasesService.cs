// <copyright file="DiseasesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Services.Mapping;

    public class DiseasesService: IDiseasesService
    {
        private readonly IRepository<Disease> diseaseRepository;
        private readonly IRepository<HealthDosierDisease> healthDosierDiseaseRepository;

        public DiseasesService(IRepository<Disease> diseaseRepository, IRepository<HealthDosierDisease> healthDosierDiseaseRepository)
        {
            this.diseaseRepository = diseaseRepository;
            this.healthDosierDiseaseRepository = healthDosierDiseaseRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Disease> query =
                this.diseaseRepository.All();
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByHealthDosier<T>(string healthDosierId)
        {
            var diseases =
                this.healthDosierDiseaseRepository.All()
                .Where(d => d.HealthDosierId == healthDosierId);

            return diseases.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var disease = this.diseaseRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return disease;
        }
    }
}
