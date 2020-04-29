// <copyright file="SymptomsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class AdministrationTextService : IAdministrationTextService
    {
        private readonly IRepository<TextFilesForAdministration> textFilesAdminRepository;

        public AdministrationTextService (IRepository<TextFilesForAdministration> textFilesAdminRepository)
        {
            this.textFilesAdminRepository = textFilesAdminRepository;
        }

        public async Task<int> CreateAsync(string name, string content)
        {
            var file = new TextFilesForAdministration
            {
                Name = name,
                Content = content,
            };

            await this.textFilesAdminRepository.AddAsync(file);
            await this.textFilesAdminRepository.SaveChangesAsync();

            return file.Id;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var file = await this.textFilesAdminRepository
                .All()
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return file;
        }

        public async Task<T> GetByNameAsync<T>(string name)
        {
            var file = await this.textFilesAdminRepository
                .All()
                .Where(s => s.Name == name)
                .To<T>()
                .FirstOrDefaultAsync();

            return file;
        }

        public async Task<int> ModifyAsync(int id, string name, string content)
        {
            var file = await this.textFilesAdminRepository
                .All()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            file.Name = name;
            file.Content = content;

            this.textFilesAdminRepository.Update(file);
            await this.textFilesAdminRepository.SaveChangesAsync();

            return file.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var file = await this.textFilesAdminRepository
                .All()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            this.textFilesAdminRepository.Delete(file);
            await this.textFilesAdminRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllinViewModelAsync<T>()
        {
            var symptoms = await this.textFilesAdminRepository
                .All()
                .To<T>()
                .ToListAsync();

            return symptoms;
        }
    }
}
