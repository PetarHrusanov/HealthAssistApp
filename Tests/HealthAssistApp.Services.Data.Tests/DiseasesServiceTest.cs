// <copyright file="DiseasesServiceTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DiseasesServiceTest : BaseServicesTests
    {
        private IDiseasesService Service => this.ServiceProvider.GetRequiredService<IDiseasesService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var diabetesId = await this.Service.CreateAsync(
                "Diabetes",
                "Malko insulin",
                "Ne qjte sladko",
                null);

            var checkModel = this.DbContext.Diseases.FirstOrDefaultAsync(a => a.Id == diabetesId);
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var diabetesId = await this.Service.CreateAsync(
                "Diabetes",
                "Malko insulin",
                "Ne qjte sladko",
                null);

            await this.Service.ModifyDiseaseAsync(
                diabetesId,
                "Diabetes Change",
                "Malko insulin update",
                "Ne qjte chak tolkova sladko",
                false);
            var checkModel = await this.DbContext.Diseases.FirstOrDefaultAsync(a => a.Id == diabetesId);
            Assert.Equal("Diabetes Change", checkModel.Name);
            Assert.Equal("Malko insulin update", checkModel.Description);
            Assert.Equal("Ne qjte chak tolkova sladko", checkModel.Advice);
        }

        [Fact]
        public async Task DeleteByIdAsync()
        {
            var diabetesId = await this.Service.CreateAsync(
                "Diabetes",
                "Malko insulin",
                "Ne qjte sladko",
                null);

            var checkModel = await this.DbContext.Diseases.FirstOrDefaultAsync(a => a.Id == diabetesId);
            Assert.NotNull(checkModel);
            await this.Service.DeleteByIdAsync(diabetesId);
            var secondCheck = await this.DbContext.Diseases.FirstOrDefaultAsync(a => a.Id == diabetesId);
            Assert.Null(secondCheck);
        }

        [Fact]
        public async Task CreateDiseaseSymptomAsync()
        {
            await this.Service.CreateDiseaseSymptomAsync(1, 2);

            var checkModel = await this.DbContext.DiseaseSymptoms
                .FirstOrDefaultAsync(a => a.DiseaseId == 1 && a.SymptomId == 2);
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task DeleteDiseaseSymptomAsync()
        {
            await this.Service.CreateDiseaseSymptomAsync(1, 2);
            await this.Service.DeleteDiseaseSymptomAsync(1, 2);
            var checkModel = await this.DbContext.DiseaseSymptoms
                .FirstOrDefaultAsync(a => a.DiseaseId == 1 && a.SymptomId == 2);
            Assert.Null(checkModel);
        }
    }
}
