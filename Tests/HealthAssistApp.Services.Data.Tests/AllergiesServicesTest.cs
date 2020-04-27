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
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

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

            var checkModel = await this.DbContext.Allergies.FirstOrDefaultAsync(a => a.Id == newAllergy);
            var getByUserId = await this.Service.GetByUserIdAsync("asd");

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

            var getByUserId = await this.Service.GetByUserIdAsync("asd");

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
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<AllergiesViewModel>(It.IsAny<Allergies>()))
                .Returns((Allergies source) => new AllergiesViewModel() {
                    UserId = source.ApplicationUserId,
                    Milk = source.Milk,
                    Eggs = source.Eggs,
                    Fish = source.Fish,
                    Crustacean = source.Crustacean,
                    TreeNuts = source.TreeNuts,
                    Peanuts = source.Peanuts,
                    Wheat = source.Wheat,
                    Soybeans = source.Soybeans,
                });

            //var mockMapperTwo = new Mock<IMapper>();
            //mockMapper.Setup(x => x.Map<Allergies>(It.IsAny<AllergiesViewModel>()))
            //    .Returns((Allergies source) => new AllergiesViewModel()
            //    {
            //        UserId = source.ApplicationUserId,
            //        Milk = source.Milk,
            //        Eggs = source.Eggs,
            //        Fish = source.Fish,
            //        Crustacean = source.Crustacean,
            //        TreeNuts = source.TreeNuts,
            //        Peanuts = source.Peanuts,
            //        Wheat = source.Wheat,
            //        Soybeans = source.Soybeans,
            //    });

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

            var getByUserId = await this.Service.GetByUserIdAsync("asd");
            var viewByUserIdModel = await this.Service.ViewByUserIdAsync<AllergiesViewModel>(getByUserId.ApplicationUserId);
            Assert.Same(checkModel, viewByUserIdModel);
        }
    }
}
