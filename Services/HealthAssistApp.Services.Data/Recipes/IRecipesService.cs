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

        Task<T> GetByIdAsyn<T>(int id);

        T GetByName<T>(string name);

        Task DeleteByIdAsync(int id);
    }
}
