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
    }
}
