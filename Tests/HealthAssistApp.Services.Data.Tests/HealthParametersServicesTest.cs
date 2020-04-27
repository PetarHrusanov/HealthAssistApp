// <copyright file="HealthParametersServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Threading.Tasks;
    using HealthAssistApp.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class HealthParametersServicesTest :BaseServicesTests
    {
        private IHealthParametersService Service => this.ServiceProvider
            .GetRequiredService<IHealthParametersService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var healthParametersId = await this.Service.CreateAsync(
                1,
                60,
                1.75m,
                "TestUser");

            var checkModel = await this.DbContext.HealthParameters
                .FirstOrDefaultAsync(h => h.Id == healthParametersId);
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task GetByUserIdAsync()
        {
            var healthParametersId = await this.Service.CreateAsync(
                1,
                60,
                1.75m,
                "TestUser");

            var checkModel = await this.DbContext.HealthParameters
                .FirstOrDefaultAsync(h => h.Id == healthParametersId);
            var actualModel = await this.Service.GetByUserIdAsync("TestUser");
            Assert.Same(checkModel, actualModel);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var healthParametersId = await this.Service.CreateAsync(
                1,
                60,
                1.75m,
                "TestUser");

            await this.Service.ModifyAsync(2, 65, 1.80m, "TestUser");
            var checkModel = await this.DbContext.HealthParameters
              .FirstOrDefaultAsync(h => h.Id == healthParametersId);
            Assert.Equal(2, checkModel.Age);
            Assert.Equal(65, checkModel.Weight);
            Assert.Equal(1.80m, checkModel.Height);
            Assert.Equal("TestUser", checkModel.ApplicationUserId);
        }

        [Fact]
        public async Task NutritionalStatusTest()
        {
            var statusUnderweight = NutritionalStatus.Underweight;
            var actualModelOne = this.Service.NutritionalStatusByBodyMassIndex(17.00m);
            Assert.Equal(statusUnderweight, actualModelOne);

            var statusCheckNormal = NutritionalStatus.Normal;
            var actualModelTwo = this.Service.NutritionalStatusByBodyMassIndex(23.00m);
            Assert.Equal(statusCheckNormal, actualModelTwo);

            var statusCheckPreObesity = NutritionalStatus.Preobesity;
            var actualModelThree = this.Service.NutritionalStatusByBodyMassIndex(25.00m);
            Assert.Equal(statusCheckPreObesity, actualModelThree);

            var statusCheckObesityI = NutritionalStatus.ObesityI;
            var actualModelFour = this.Service.NutritionalStatusByBodyMassIndex(32.00m);
            Assert.Equal(statusCheckObesityI, actualModelFour);

            var statusCheckObesityII = NutritionalStatus.ObesityII;
            var actualModelFive = this.Service.NutritionalStatusByBodyMassIndex(36.00m);
            Assert.Equal(statusCheckObesityII, actualModelFive);

            var statusCheckObesityIII = NutritionalStatus.ObesityIII;
            var actualModelSix = this.Service.NutritionalStatusByBodyMassIndex(42.00m);
            Assert.Equal(statusCheckObesityIII, actualModelSix);
        }
    }
}
