// <copyright file="IFoodRegimensService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HealthAssistApp.Data.Models;

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

        public Task<int> GetRegimenByHealthDosierId(string healthDosierId);

        IEnumerable<T> GetMealsByFoodRegimenId<T>(int foodRegimenId, int? take = null, int skip = 0);
    }
}
