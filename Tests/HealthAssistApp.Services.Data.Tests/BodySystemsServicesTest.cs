// <copyright file="BodySystemsServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using System.Linq;
using System.Threading.Tasks;
using HealthAssistApp.Services.Data.BodySystems;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HealthAssistApp.Services.Data.Tests
{
    public class BodySystemsServicesTest : BaseServicesTests
    {
        private IBodySystemsService Service => this.ServiceProvider.GetRequiredService<IBodySystemsService>();

        [Fact]
        public async Task CreateAsync()
        {
            var bodySystemId = await this.Service.CreateAsync("Test System");

            var checkModel = this.DbContext.BodySystems.FirstOrDefault(a => a.Id == bodySystemId);
            Assert.False(checkModel == null);
        }

        //[Fact]
        //public async Task GetByUserIdAsync()
        //{
        //    var newAllergy = await Service.CreateAsync(
        //        true,
        //        true,
        //        true,
        //        true,
        //        false,
        //        false,
        //        false,
        //        false,
        //        "asd");

        //    var checkModel = this.DbContext.Allergies.FirstOrDefault(a => a.Id == newAllergy);
        //    var getByUserId = this.Service.GetByUserId("asd");

        //    Assert.Same(checkModel, getByUserId);
        //}

        //[Fact]
        //public async Task ModifyAsync()
        //{
        //    var newAllergy = await Service.CreateAsync(
        //        true,
        //        true,
        //        true,
        //        true,
        //        false,
        //        false,
        //        false,
        //        false,
        //        "asd");

        //    var checkModel = this.DbContext.Allergies.FirstOrDefault(a => a.Id == newAllergy);
        //    var modifyUser = this.Service.ModifyAsync(
        //        false,
        //        false,
        //        false,
        //        false,
        //        false,
        //        false,
        //        false,
        //        false,
        //        "asd");

        //    var getByUserId = this.Service.GetByUserId("asd");

        //    Assert.False(getByUserId.Milk);
        //    Assert.False(getByUserId.Eggs);
        //    Assert.False(getByUserId.Fish);
        //    Assert.False(getByUserId.Crustacean);
        //    Assert.False(getByUserId.TreeNuts);
        //    Assert.False(getByUserId.Peanuts);
        //    Assert.False(getByUserId.Wheat);
        //    Assert.False(getByUserId.Soybeans);
        //}
    }
}
