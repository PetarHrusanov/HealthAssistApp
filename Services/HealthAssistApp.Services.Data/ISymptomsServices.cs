// <copyright file="ISymptomsServices.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISymptomsServices
    {
        Task<int> CreateUserSymptomAsync(string description, string systemName, string userId);

        Task<IEnumerable<string>> GetSystemNameFromUserId(string userId);
    }
}
