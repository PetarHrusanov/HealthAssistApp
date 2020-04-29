// <copyright file="ISymptomsServices.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISymptomsServices
    {
        Task<int> CreateSymptomAsync(string description, int bodySystemId);

        Task<T> GetModelByIdAsync<T>(int id);

        Task<int> ModifySymptomAsync(int symptomId, string description, int bodySystemId);

        Task DeleteSymptomAsync(int id);

        Task<IEnumerable<T>> GetAllinViewModelAsync<T>();

        IEnumerable<T> SymptomsDropDownMenu<T>();

        Task<int> CreateUserSymptomAsync(string description, string systemName, string userId);

        Task DeleteUserSymptomsAsync(string id);

        Task<IEnumerable<string>> GetSystemNameFromUserId(string userId);
    }
}
