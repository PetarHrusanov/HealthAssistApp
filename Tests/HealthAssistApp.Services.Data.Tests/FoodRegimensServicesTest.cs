// <copyright file="FoodRegimensServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>
namespace HealthAssistApp.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class FoodRegimensServicesTest : BaseServicesTests
    {
        private IFoodRegimensService Service => this.ServiceProvider.GetRequiredService<IFoodRegimensService>();

        [Fact]
        public async Task CreateRegimenAsync()
        {
            var foodRegimenId = await this.Service.CreateFoodRegimenAsync(
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true);

            var checkModel = await this.DbContext.FoodRegimens.FirstOrDefaultAsync(a => a.Id == foodRegimenId);
            Assert.NotNull(checkModel);
        }

        //Task DeleteMealsById(int id);

        //Task DeleteByIdAsync(int id);

        //Task<int> GetRegimenByHealthDosierIdAsync(string healthDosierId);

        //IEnumerable<T> GetMealsByFoodRegimenId<T>(int foodRegimenId, int? take = null, int skip = 0);
        }
}
