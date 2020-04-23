// <copyright file="AllergiesServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Xunit;
    using HealthAssistApp.Web.ViewModels;
    using HealthAssistApp.Web.ViewModels.Allergies;

    public class AllergiesServicesTest : BaseServicesTests
    {
        private IAllergiesService Service => this.ServiceProvider.GetRequiredService<IAllergiesService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var newAllergy = await Service.CreateAsync(
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                "asd");

            var checkModel = this.DbContext.Allergies.FirstOrDefault(a => a.Id == newAllergy);
            Assert.False(checkModel == null);
        }

        [Fact]
        public async Task GetByUserIdAsyncTest()
        {
            var newAllergy = await Service.CreateAsync(
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                "asd");

            var checkModel = this.DbContext.Allergies.FirstOrDefault(a => a.Id == newAllergy);
            var getByUserId = this.Service.GetByUserId("asd");

            Assert.Same(checkModel, getByUserId);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var newAllergy = await Service.CreateAsync(
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                "asd");

            var checkModel = this.DbContext.Allergies.FirstOrDefault(a => a.Id == newAllergy);
            var modifyUser = this.Service.ModifyAsync(
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                "asd");

            var getByUserId = this.Service.GetByUserId("asd");

            Assert.False(getByUserId.Milk);
            Assert.False(getByUserId.Eggs);
            Assert.False(getByUserId.Fish);
            Assert.False(getByUserId.Crustacean);
            Assert.False(getByUserId.TreeNuts);
            Assert.False(getByUserId.Peanuts);
            Assert.False(getByUserId.Wheat);
            Assert.False(getByUserId.Soybeans);
        }

        [Fact]
        public async Task ViewByIdAsyncTest()
        {
            var newAllergy = await this.Service.CreateAsync(
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                "asd");

            var checkModel = new AllergiesViewModel
            {
                Milk = true,
                Eggs = true,
                Fish = true,
                Crustacean = true,
                TreeNuts = false,
                Peanuts = false,
                Wheat = false,
                Soybeans = false,
                UserId = "asd",
            };

            var getByUserId = this.Service.GetByUserId("asd");
            var viewByUserIdModel = await this.Service.ViewByUserIdAsync<AllergiesViewModel>(getByUserId.ApplicationUserId);
            Assert.Same(checkModel, viewByUserIdModel);
        }
    }
}
