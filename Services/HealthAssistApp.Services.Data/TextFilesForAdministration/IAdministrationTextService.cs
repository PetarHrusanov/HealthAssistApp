// <copyright file="ISymptomsServices.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdministrationTextService
    {
        Task<int> CreateAsync(string name, string content);

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        Task<int> ModifyAsync(int id, string name, string content);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllinViewModelAsync<T>();
    }
}
