// <copyright file="IBodySystemsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System.Threading.Tasks;
using HealthAssistApp.Data.Models;

namespace HealthAssistApp.Services.Data.BodySystems
{
    public interface IBodySystemsService
    {
        public Task DeleteByIdAsync(int bodySystemId);

        public Task<BodySystem> GetByIdAsync(int bodySystemId)
    }
}
