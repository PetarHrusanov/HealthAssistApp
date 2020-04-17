// <copyright file="RecipesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Services.Mapping;

    public class RecipesService : IRecipesService
    {
        private readonly IRepository<Recipe> recipesRepository;
        //private readonly ApplicationDbContext dbContext;

        public RecipesService(IRepository<Recipe> recipesRepository)
        {
            this.recipesRepository = recipesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Recipe> query =
                this.recipesRepository.All();
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var recipe = this.recipesRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return recipe;
        }
    }
}