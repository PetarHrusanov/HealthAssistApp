// <copyright file="RecipesServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class RecipesServicesTest : BaseServicesTests
    {
        private IRecipesService Service => this.ServiceProvider.GetRequiredService<IRecipesService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var recipesId = await this.Service.CreateAsync(
                "Chicken",
                "Cut the chicken and boil it",
                "randomUrl",
                false,
                false,
                PartOfMeal.Snack,
                GlycemicIndex.Medium,
                100);

            var checkModel = await this.DbContext.Recipes.FirstOrDefaultAsync(a => a.Id == recipesId);
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var recipesId = await this.Service.CreateAsync(
                "Chicken",
                "Cut the chicken and boil it",
                "randomUrl",
                false,
                false,
                PartOfMeal.Snack,
                GlycemicIndex.Medium,
                100);

            await this.Service.ModifyAsync(
                recipesId,
                "Pork",
                "Cut the pork and boil it",
                "newUrl",
                false,
                false,
                PartOfMeal.Snack,
                GlycemicIndex.Medium,
                110);

            var checkModel = await this.DbContext.Recipes.FirstOrDefaultAsync(a => a.Id == recipesId);
            Assert.Equal("Pork", checkModel.Name);
            Assert.Equal("Cut the pork and boil it", checkModel.InstructionForPreparation);
            Assert.Equal("newUrl", checkModel.ImageUrl);
            Assert.False(checkModel.Vegan);
            Assert.False(checkModel.Vegetarian);
            Assert.Equal(PartOfMeal.Snack, checkModel.PartOfMeal);
            Assert.Equal(GlycemicIndex.Medium, checkModel.GlycemicIndex);
            Assert.Equal(110, checkModel.Calories);
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            var recipesId = await this.Service.CreateAsync(
                "Chicken",
                "Cut the chicken and boil it",
                "randomUrl",
                false,
                false,
                PartOfMeal.Snack,
                GlycemicIndex.Medium,
                100);

            var checkModel = await this.DbContext.Recipes.FirstOrDefaultAsync(a => a.Id == recipesId);
            Assert.NotNull(checkModel);
            await this.Service.DeleteByIdAsync(recipesId);
            var secondCheck = await this.DbContext.Recipes.FirstOrDefaultAsync(a => a.Id == recipesId);
            Assert.Null(secondCheck);
        }
    }
}
