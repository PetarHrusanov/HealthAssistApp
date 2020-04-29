// <copyright file="IRecipesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models.Enums;

    public interface IRecipesService
    {
        Task<int> CreateAsync(
            string name,
            string instructionForPreparation,
            string imageUrl,
            bool vegan,
            bool vegetarian,
            PartOfMeal partOfMeal,
            GlycemicIndex glycemicIndex,
            int calories);

        Task<IEnumerable<T>> GetAllAsync<T>();

        IEnumerable<T> GetAllPaginatedAsync<T>(int? take, int skip);

        Task<int> GetRecipesCountAsync();

        Task<T> GetByIdAsyn<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        Task<int> ModifyAsync(
            int id,
            string name,
            string instructionForPreparation,
            string imageUrl,
            bool vegan,
            bool vegetarian,
            PartOfMeal partOfMeal,
            GlycemicIndex glycemicIndex,
            int calories);

        Task DeleteByIdAsync(int id);
    }
}
