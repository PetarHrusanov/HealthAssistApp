// <copyright file="FoodRegimensServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class FoodRegimensServicesTest : BaseServicesTests
    {
        private IFoodRegimensService Service => this.ServiceProvider.GetRequiredService<IFoodRegimensService>();



        //[Fact]
        //public async Task DeleteByIdAsync()
        //{
        //    var newAllergy = await Service.CreateAsync(
        //         true,
        //         true,
        //         true,
        //         true,
        //         false,
        //         false,
        //         false,
        //         false,
        //         "TestUser");

        //    var checkModel = this.DbContext.Allergies.FirstOrDefault(a => a.Id == newAllergy);
        //    Assert.NotNull(checkModel);

        //    await this.Service.DeleteByUserIdAsync("TestUser");
        //    var secondCheckModel = await this.DbContext.HealthParameters
        //        .FirstOrDefaultAsync(h => h.Id == newAllergy);
        //    Assert.Null(secondCheckModel);
        //}
    }
}
