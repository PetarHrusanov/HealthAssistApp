// <copyright file="BodySystemsServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Data.BodySystems;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class BodySystemsServicesTest : BaseServicesTests
    {
        private IBodySystemsService Service => this.ServiceProvider.GetRequiredService<IBodySystemsService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var bodySystemId = await this.Service.CreateAsync("Test System");

            var checkModel = this.DbContext.BodySystems.FirstOrDefault(a => a.Id == bodySystemId);
            Assert.False(checkModel == null);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var bodySystemId = await this.Service.CreateAsync("Test System");

            await this.Service.ModifyAsync(bodySystemId, "Test System Change");

            var bodySystem = this.DbContext.BodySystems.FirstOrDefault(b => b.Id == bodySystemId);
            Assert.Equal("Test System Change", bodySystem.Name);
        }

        [Fact]
        public async Task GetByIdAsyncTest()
        {
            var bodySystemId = await this.Service.CreateAsync("Test System");

            var bodySystem = this.Service.GetById<BodySystem>(bodySystemId);
            Assert.NotNull(bodySystem);
        }

        [Fact]
        public async Task DeleteByUserIdAsyncTest()
        {
            var bodySystemId = await this.Service.CreateAsync("Test System");
            var bodySystem = this.DbContext.BodySystems.FirstOrDefault(b => b.Id == bodySystemId);
            Assert.False(bodySystem == null);
            await this.Service.DeleteByIdAsync(bodySystem.Id);
            var deletedBodySystem = this.DbContext.BodySystems.FirstOrDefault(b => b.Id == bodySystemId);
            Assert.Null(bodySystem);
        }
    }
}
