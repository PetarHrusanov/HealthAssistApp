// <copyright file="IHealthParametersService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;

    public interface IHealthParametersService
    {
        T ViewByUserId<T>(string userId);

        HealthParameters GetByUserId(string userId);

        Task<int> ModifyAsync(
            int age,
            int weight,
            decimal height,
            decimal bodyMassIndex,
            decimal waterPerDay,
            string userId);

        Task<int> CreateAsync(
            int age,
            int weight,
            decimal height,
            decimal bodyMassIndex,
            decimal waterPerDay,
            string userId);
    }
}
