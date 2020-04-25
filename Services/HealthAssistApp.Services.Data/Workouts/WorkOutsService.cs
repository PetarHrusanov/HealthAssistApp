﻿// <copyright file="WorkOutsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Data.Models.WorkingOut;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class WorkOutsService : IWorkOutsService
    {
        private readonly IRepository<WorkoutProgram> workoutRepository;
        private readonly IRepository<Exercise> exercisesRepository;
        private readonly IRepository<ExerciseWorkoutProgram> exercisesWorkoutsRepository;
        private readonly IRepository<HealthDosier> healthDosierRepository;

        public WorkOutsService(
            IRepository<WorkoutProgram> workoutRepository,
            IRepository<Exercise> exercisesRepository,
            IRepository<ExerciseWorkoutProgram> exercisesWorkoutsRepository,
            IRepository<HealthDosier> healthDosierRepository)
        {
            this.workoutRepository = workoutRepository;
            this.exercisesRepository = exercisesRepository;
            this.exercisesWorkoutsRepository = exercisesWorkoutsRepository;
            this.healthDosierRepository = healthDosierRepository;
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

        public IEnumerable<T> GetExercisesByWorkoutId<T>(int workoutprogramId, int? take = null, int skip = 0)
        {
            var query = this.exercisesWorkoutsRepository.All()
                .Where(x => x.WorkoutProgramId == workoutprogramId)
                .Select(e => e.Exercise)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<int> GetExercisesCountByWorkoutId(int workoutId)
        {
            var exercises = await this.exercisesWorkoutsRepository
                .All()
                .Where(w => w.WorkoutProgramId == workoutId)
                .Select(e => e.Exercise)
                .ToListAsync();

            return exercises.Count();
        }

        public async Task<int> GetWorkoutProgramsByHealthDosierId(string healthDosierId)
        {
            var workoutProgramId = await this.healthDosierRepository
                .All()
                .Where(h => h.Id == healthDosierId)
                .Select(w => w.WorkoutProgram)
                .FirstAsync();

            return workoutProgramId.Id;
        }
    }
}