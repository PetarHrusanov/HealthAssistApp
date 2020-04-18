// <copyright file="HealthParametersService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;

    public class HealthParametersService : IHealthParametersService
    {
        public HealthParametersService()
        {
        }

        public Task<int> CreateAsync(int age, int weight, decimal height, decimal bodyMassIndex, decimal waterPerDay, string userId)
        {
            throw new NotImplementedException();
        }

        public HealthParameters GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> ModifyAsync(int age, int weight, decimal height, decimal bodyMassIndex, decimal waterPerDay, string userId)
        {
            throw new NotImplementedException();
        }

        public T ViewByUserId<T>(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
