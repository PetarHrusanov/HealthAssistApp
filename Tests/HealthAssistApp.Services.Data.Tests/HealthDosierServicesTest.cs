// <copyright file="HealthDosierServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class HealthDosierServicesTest : BaseServicesTests
    {
        private IHealthDosiersService Service => this.ServiceProvider.GetRequiredService<IHealthDosiersService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var healthDosierId = await this.Service.CreateHealthDosierAsync(
                1,
                1,
                1,
                false,
                false,
                1,
                "User");

            var checkModel = await this.DbContext.HealthDosiers.FirstOrDefaultAsync(h => h.Id == healthDosierId);
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task GetByUserIdAsync()
        {
            var healthDosierId = await this.Service.CreateHealthDosierAsync(
                1,
                1,
                1,
                false,
                false,
                1,
                "User");

            var checkModel = await this.DbContext.HealthDosiers.FirstOrDefaultAsync(h => h.Id == healthDosierId);
            var actualModel = await this.Service.GetByUserIdAsync("User");
            Assert.Same(checkModel, actualModel);
        }

        [Fact]
        public async Task UserSideDelete()
        {
            var healthDosierId = await this.Service.CreateHealthDosierAsync(
                1,
                1,
                1,
                false,
                false,
                1,
                "User");

            var checkModel = await this.DbContext.HealthDosiers.FirstOrDefaultAsync(h => h.Id == healthDosierId);
            Assert.NotNull(checkModel);
            await this.Service.UserSideDeleteAsync(healthDosierId);
            var secondCheckModel = await this.DbContext.HealthDosiers.FirstOrDefaultAsync(h => h.Id == healthDosierId);
            Assert.Null(secondCheckModel);
        }
    }
}
