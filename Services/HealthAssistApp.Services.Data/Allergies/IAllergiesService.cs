﻿// <copyright file="IAllergiesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Threading.Tasks;
    using HealthAssistApp.Data.Models.FoodModels;

    public interface IAllergiesService
    {
        Task<int> CreateAsync(
            bool milk,
            bool eggs,
            bool fish,
            bool crustacean,
            bool treenuts,
            bool peanuts,
            bool wheat,
            bool soybeans,
            string userId);

        Task<Allergies> GetByUserIdAsync(string userId);

        Task<int> ModifyAsync(
            bool milk,
            bool eggs,
            bool fish,
            bool crustacean,
            bool treenuts,
            bool peanuts,
            bool wheat,
            bool soybeans,
            string userId);

        Task<T> ViewByUserIdAsync<T>(string userId);
    }
}
