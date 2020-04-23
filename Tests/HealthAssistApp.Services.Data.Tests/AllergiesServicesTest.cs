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

    public class AllergiesServicesTest : BaseServicesTests
    {
        private IAllergiesService Service => this.ServiceProvider.GetRequiredService<IAllergiesService>();

        [Fact]
        public async Task CreateAsync()
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
        public async Task GetByUserIdAsync()
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
        public async Task ModifyAsync()
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
        public async Task ViewByIdAsync()
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
                ApplicationUserId = "asd",
            };

            //var checkModel = this.Service.GetByUserId("ads");

            AllergiesViewModel viewByUserIdModel = this.Service.ViewByUserId<AllergiesViewModel>("asd");
            Assert.Same(checkModel, viewByUserIdModel);
        }
    }

    public class AllergiesViewModel : IMapFrom<Allergies>
    {
        public bool Milk { get; set; }

        public bool Eggs { get; set; }

        public bool Fish { get; set; }

        public bool Crustacean { get; set; }

        public bool TreeNuts { get; set; }

        public bool Peanuts { get; set; }

        public bool Wheat { get; set; }

        public bool Soybeans { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
