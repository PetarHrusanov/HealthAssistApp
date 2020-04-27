// <copyright file="SymptomsServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SymptomsServicesTest : BaseServicesTests
    {
        private ISymptomsServices Service => this.ServiceProvider.GetRequiredService<ISymptomsServices>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var symptomId = await this.Service.CreateSymptomAsync(
                "Test Pain",
                1);

            var checkModel = await this.DbContext.Symptoms.FirstOrDefaultAsync(a => a.Id == symptomId);
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var symptomId = await this.Service.CreateSymptomAsync(
                "Test Pain",
                1);

            await this.Service.ModifySymptomAsync(
                symptomId,
                "More Test Pain",
                2);

            var checkModel = await this.DbContext.Symptoms.FirstOrDefaultAsync(a => a.Id == symptomId);

            Assert.Equal("More Test Pain", checkModel.Description);
            Assert.Equal(2, checkModel.BodySystemId);
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            var symptomId = await this.Service.CreateSymptomAsync(
                "Test Pain",
                1);

            var checkModel = await this.DbContext.Symptoms.FirstOrDefaultAsync(a => a.Id == symptomId);
            Assert.NotNull(checkModel);

            await this.Service.DeleteSymptomAsync(symptomId);
            var checkModelTwo = await this.DbContext.Symptoms.FirstOrDefaultAsync(a => a.Id == symptomId);
            Assert.Null(checkModelTwo);
        }

        [Fact]
        public async Task CreateUserSymptomAsyncTest()
        {
            var symptomId = await this.Service.CreateUserSymptomAsync(
                "Test Pain",
                "Body System",
                "Test User");

            var checkModel = await this.DbContext.UserSymptoms.FirstOrDefaultAsync(a => a.Id == symptomId);
            Assert.NotNull(checkModel);
        }
    }
}
