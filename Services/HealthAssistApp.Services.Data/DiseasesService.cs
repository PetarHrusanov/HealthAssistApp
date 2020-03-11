namespace HealthAssistApp.Services.Data
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class DiseasesService: IDiseasesService
    {
        private readonly IRepository<Disease> diseaseRepository;

        public DiseasesService(IRepository<Disease> diseaseRepository)
        {
            this.diseaseRepository = diseaseRepository;
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

        public T GetByName<T>(string name)
        {
            var disease = this.diseaseRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return disease;
        }
    }
}
