// <copyright file="IFoodRegimensService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFoodRegimensService
    {
        Task<int> CreateFoodRegimenAsync(
            bool milk,
            bool eggs,
            bool fish,
            bool crustacean,
            bool treenuts,
            bool peanuts,
            bool wheat,
            bool soybeans);

        Task DeleteMealsById(int id);

        Task DeleteByIdAsync(int id);

        Task<int> GetRegimenByHealthDosierIdAsync(string healthDosierId);

        IEnumerable<T> GetMealsByFoodRegimenId<T>(int foodRegimenId, int? take = null, int skip = 0);
    }
}
