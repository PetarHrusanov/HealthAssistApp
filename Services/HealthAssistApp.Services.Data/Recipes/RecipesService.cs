// <copyright file="RecipesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class RecipesService : IRecipesService
    {
        private readonly IRepository<Recipe> recipesRepository;
        // private readonly ApplicationDbContext dbContext;

        public RecipesService(IRepository<Recipe> recipesRepository)
        {
            this.recipesRepository = recipesRepository;
        }

        public async Task<int> CreateAsync(
            string name,
            string instructionForPreparation,
            string imageUrl,
            bool vegan,
            bool vegetarian,
            PartOfMeal partOfMeal,
            GlycemicIndex glycemicIndex,
            int calories)
        {
            var recipe = new Recipe
            {
                Name = name,
                InstructionForPreparation = instructionForPreparation,
                ImageUrl = imageUrl,
                Vegan = vegan,
                Vegetarian = vegetarian,
                PartOfMeal = partOfMeal,
                GlycemicIndex = glycemicIndex,
                Calories = calories,
            };
            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
            return recipe.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var query = await
                this.recipesRepository
                .All()
                .To<T>()
                .ToListAsync();

            return query;
        }

        public IEnumerable<T> GetAllPaginatedAsync<T>(int? take, int skip)
        {
            var query = this.recipesRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<int> GetRecipesCountAsync()
        {
            var diseases = this.recipesRepository
                .All();

            return diseases.Count();
        }

        public async Task<T> GetByIdAsyn<T>(int id)
        {
            var recipe = await this.recipesRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            return recipe;
        }

        public async Task<T> GetByNameAsync<T>(string name)
        {
            var recipe = await this.recipesRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefaultAsync();
            return recipe;
        }

        public async Task<int> ModifyAsync(
            int id,
            string name,
            string instructionForPreparation,
            string imageUrl,
            bool vegan,
            bool vegetarian,
            PartOfMeal partOfMeal,
            GlycemicIndex glycemicIndex,
            int calories)
        {
            var recipe = await this.recipesRepository
                .All()
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
            recipe.Name = name;
            recipe.InstructionForPreparation = instructionForPreparation;
            recipe.ImageUrl = imageUrl;
            recipe.Vegan = vegan;
            recipe.Vegetarian = vegetarian;
            recipe.PartOfMeal = partOfMeal;
            recipe.GlycemicIndex = glycemicIndex;
            recipe.Calories = calories;

            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();
            return recipe.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var recipe = await this.recipesRepository
                .All()
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            this.recipesRepository.Delete(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
