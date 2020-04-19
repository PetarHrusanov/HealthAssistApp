// <copyright file="WorkOutsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Data.Models.WorkingOut;

    public class WorkOutsService : IWorkOutsService
    {
        private readonly IRepository<WorkoutProgram> workoutRepository;
        private readonly IRepository<Exercise> exercisesRepository;
        private readonly IRepository<ExerciseWorkoutProgram> exercisesWorkoutsRepository;

        public WorkOutsService(
            IRepository<WorkoutProgram> workoutRepository,
            IRepository<Exercise> exercisesRepository,
            IRepository<ExerciseWorkoutProgram> exercisesWorkoutsRepository)
        {
            this.workoutRepository = workoutRepository;
            this.exercisesRepository = exercisesRepository;
            this.exercisesWorkoutsRepository = exercisesWorkoutsRepository;
        }

        public async Task<int> CreateWorkoutProgramAsync(ExerciseComplexity complexity, string userId)
        {
            var workoutProgram = new WorkoutProgram
            {
                ExerciseComplexity = complexity,
                ApplicationUserId = userId,
            };

            await this.workoutRepository.AddAsync(workoutProgram);
            await this.workoutRepository.SaveChangesAsync();

            List<Exercise> exercises = this.exercisesRepository
                .All()
                .Where(e => e.ExerciseComplexity == complexity)
                .ToList();

            foreach (var exercise in exercises)
            {
                var exercisesWorkoutProgram = new ExerciseWorkoutProgram
                {
                    ExerciseId = exercise.Id,
                    WorkoutProgramId = workoutProgram.Id,
                };

                await this.exercisesWorkoutsRepository.AddAsync(exercisesWorkoutProgram);
                await this.exercisesWorkoutsRepository.SaveChangesAsync();
            }

            return workoutProgram.Id;
        }
    }
}
