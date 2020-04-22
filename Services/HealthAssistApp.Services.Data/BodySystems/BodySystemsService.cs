// <copyright file="BodySystemsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.BodySystems
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BodySystemsService : IBodySystemsService
    {
        public readonly IRepository<BodySystem> bodySystemsRepository;

        public BodySystemsService(IRepository<BodySystem> bodySystemsRepository)
        {
            this.bodySystemsRepository = bodySystemsRepository;
        }

        public async Task DeleteByIdAsync(int bodySystemId)
        {
            var bodySystem = await this.bodySystemsRepository
                .All()
                .FirstOrDefaultAsync(b => b.Id == bodySystemId);
            if (bodySystem != null)
            {
                bodySystemsRepository.Delete(bodySystem);
                await this.bodySystemsRepository.SaveChangesAsync();
            }
        }

        public async Task<BodySystem> GetByIdAsync(int bodySystemId)
        {
            var bodySystem = await this.bodySystemsRepository
                .All()
                .FirstOrDefaultAsync(b => b.Id == bodySystemId);
            return bodySystem;
        }
    }
}
