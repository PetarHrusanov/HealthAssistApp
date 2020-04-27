// <copyright file="IHealthParametersService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;

    public interface IHealthParametersService
    {
        T ViewByUserId<T>(string userId);

        Task<HealthParameters> GetByUserIdAsync(string userId);

        Task<int> ModifyAsync(
            int age,
            int weight,
            decimal height,
            string userId);

        Task<int> CreateAsync(
            int age,
            int weight,
            decimal height,
            string userId);

        NutritionalStatus NutritionalStatusByBodyMassIndex(decimal bodyMassIndex);
    }
}
