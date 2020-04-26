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

        [Fact]
        public async Task CreateWorkoutProgramAsync()
        {
            var exerciseOne = await this.Service.CreateExerciseAsync(
                "Push Up-s",
                "Place your hands on the ground",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium);

            var checkModelOne = await this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseOne);
            Assert.NotNull(checkModelOne);

            var exerciseTw0 = await this.Service.CreateExerciseAsync(
                "Jumps",
                "Jump a few times",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium);

            var checkModelTwo = await this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseTw0);
            Assert.NotNull(checkModelTwo);

            var exerciseThree = await this.Service.CreateExerciseAsync(
                "Running",
                "Run for 2 km",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium);

            var checkModelThree = await this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseThree);
            Assert.NotNull(checkModelThree);

            var exerciseFour = await this.Service.CreateExerciseAsync(
                "Sit-ups",
                "Do three sit-ups",
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Low);

            var checkModelFour = await this.DbContext.Exercises.FirstOrDefaultAsync(a => a.Id == exerciseFour);
            Assert.NotNull(checkModelThree);

            var workOutProgramId = await this.Service.CreateWorkoutProgramAsync(
                HealthAssistApp.Data.Models.Enums.ExerciseComplexity.Medium,
                "TestUserId");

            var checkWorkoutsModel = this.DbContext.WorkoutPrograms.FirstOrDefaultAsync(a => a.Id == workOutProgramId);
            Assert.NotNull(checkWorkoutsModel);
        }

        // imam da napravq oshte mnogo services tuk 
    }
}
