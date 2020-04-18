// <copyright file="ISymptomsServices.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System.Threading.Tasks;

namespace HealthAssistApp.Services.Data
{
    public interface ISymptomsServices
    {
        Task<int> CreateUserSymptomAsync(string description, string systemName, string userId);
    }
}
