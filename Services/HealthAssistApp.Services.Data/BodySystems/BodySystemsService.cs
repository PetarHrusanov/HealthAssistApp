﻿// <copyright file="BodySystemsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.BodySystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class BodySystemsService : IBodySystemsService
    {
        public readonly IRepository<BodySystem> bodySystemsRepository;

        public BodySystemsService(IRepository<BodySystem> bodySystemsRepository)
        {
            this.bodySystemsRepository = bodySystemsRepository;
        }

        public async Task<int> CreateAsync(string name)
        {
            var bodySystem = new BodySystem { Name = name };
            await this.bodySystemsRepository.AddAsync(bodySystem);
            await this.bodySystemsRepository.SaveChangesAsync();
            return bodySystem.Id;
        }

        public async Task<T> GetById<T>(int bodySystemId)
        {
            var bodySystem = await this.bodySystemsRepository
                .All()
                .Where(b => b.Id == bodySystemId)
                .To<T>()
                .FirstOrDefaultAsync();
            return bodySystem;
        }

        public async Task<int> ModifyAsync(int id, string name)
        {
            var bodySystem = await this.bodySystemsRepository
                .All()
                .FirstOrDefaultAsync(b => b.Id == id);
            bodySystem.Name = name;
            this.bodySystemsRepository.Update(bodySystem);
            await this.bodySystemsRepository.SaveChangesAsync();
            return bodySystem.Id;
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

        public async Task<IEnumerable<T>> BodySystemDropDownMenu<T>()
        {
            var disease = await this.bodySystemsRepository
                .All()
                .To<T>()
                .ToListAsync();

            return disease;
        }
    }
}
