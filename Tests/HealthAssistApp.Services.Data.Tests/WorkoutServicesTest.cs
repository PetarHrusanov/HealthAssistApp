namespace HealthAssistApp.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class WorkoutServicesTest : BaseServicesTests
    {
        private IWorkOutsService Service => this.ServiceProvider.GetRequiredService<IWorkOutsService>();

        [Fact]
        public async Task CreateAsyncTest()
        {
            var exerciseId = await this.Service.CreateExerciseAsync(
                "Push Up-s",
                "Place your hands on the ground",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium);

            var checkModel = this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseId);
            Assert.NotNull(checkModel);
        }

        [Fact]
        public async Task ModifyAsyncTest()
        {
            var exerciseId = await this.Service.CreateExerciseAsync(
                "Push Up-s",
                "Place your hands on the ground",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium);

            await this.Service.ModifyAsync(
                exerciseId,
                "Push Up-s Edit",
                "Place your hands on the ground & Push",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium);

            var checkModel = await this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseId);
            Assert.Equal("Push Up-s Edit", checkModel.Name);
            Assert.Equal("Place your hands on the ground & Push", checkModel.Instructions);
            Assert.Equal(HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium, checkModel.ExerciseComplexity);
        }

        [Fact]
        public async Task DeleteByIdAsync()
        {
            var exerciseId = await this.Service.CreateExerciseAsync(
                "Push Up-s",
                "Place your hands on the ground",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium);

            var checkModel = this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseId);
            Assert.NotNull(checkModel);
            await this.Service.DeleteByIdAsync(exerciseId);
            var secondCheck = await this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseId);
            Assert.Null(secondCheck);
        }

        //[Fact]
        //public async Task CreateDiseaseSymptomAsync()
        //{
        //    await this.Service.CreateDiseaseSymptomAsync(1, 2);

        //    var checkModel = await this.DbContext.DiseaseSymptoms
        //        .FirstOrDefaultAsync(a => a.DiseaseId == 1 && a.SymptomId == 2);
        //    Assert.NotNull(checkModel);
        //}

        //[Fact]
        //public async Task DeleteDiseaseSymptomAsync()
        //{
        //    await this.Service.CreateDiseaseSymptomAsync(1, 2);
        //    await this.Service.DeleteDiseaseSymptomAsync(1, 2);
        //    var checkModel = await this.DbContext.DiseaseSymptoms
        //        .FirstOrDefaultAsync(a => a.DiseaseId == 1 && a.SymptomId == 2);
        //    Assert.Null(checkModel);
        //}
    }
}
