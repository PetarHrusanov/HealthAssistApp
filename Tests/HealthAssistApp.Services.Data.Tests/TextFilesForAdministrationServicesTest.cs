// <copyright file="TextFilesForAdministrationServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class TextFilesForAdministrationServicesTest :BaseServicesTests
    {
        private IAdministrationTextService Service => this.ServiceProvider.GetRequiredService<IAdministrationTextService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var id = await this.Service.CreateAsync(
                "Test Doc",
                "Test Content");

            var checkModel = await this.DbContext
                .TextFilesForAdministration
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var id = await this.Service.CreateAsync(
                "Test Doc",
                "Test Content");

            await this.Service.ModifyAsync(
                id,
                "Test Doc New",
                "Test Content Update");

            var checkModel = await this.DbContext.TextFilesForAdministration.FirstOrDefaultAsync(a => a.Id == id);

            Assert.Equal("Test Doc New", checkModel.Name);
            Assert.Equal("Test Content Update", checkModel.Content);
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            var id = await this.Service.CreateAsync(
                "Test Doc",
                "Test Content");

            var checkModel = await this.DbContext.TextFilesForAdministration.FirstOrDefaultAsync(a => a.Id == id);
            Assert.NotNull(checkModel);

            await this.Service.DeleteAsync(id);
            var secondCheckModel = await this.DbContext.TextFilesForAdministration.FirstOrDefaultAsync(a => a.Id == id);
            Assert.NotNull(secondCheckModel);
        }

        //Task<T> GetByIdAsync<T>(int id);

        //Task<T> GetByNameAsync<T>(string name);

        //Task<IEnumerable<T>> GetAllinViewModelAsync<T>();
    }
}
