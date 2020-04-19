// <copyright file="IFoodRegimensService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
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
    }
}
