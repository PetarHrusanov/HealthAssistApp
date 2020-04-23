// <copyright file="IBodySystemsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>
namespace HealthAssistApp.Services.Data.BodySystems
{
    using System;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;

    public interface IBodySystemsService
    {
        public Task<int> CreateAsync(string name);

        public T GetById<T>(int bodySystemId);

        public Task<int> ModifyAsync(int id, string name);

        public Task DeleteByIdAsync(int bodySystemId);

    }
}
