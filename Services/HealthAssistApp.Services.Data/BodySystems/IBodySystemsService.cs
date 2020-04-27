// <copyright file="IBodySystemsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>
namespace HealthAssistApp.Services.Data.BodySystems
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;

    public interface IBodySystemsService
    {
        Task<int> CreateAsync(string name);

        Task<T> GetById<T>(int bodySystemId);

        Task<int> ModifyAsync(int id, string name);

        Task DeleteByIdAsync(int bodySystemId);

        Task<IEnumerable<T>> BodySystemDropDownMenu<T>();
    }
}
