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

        public async Task<int> CreateExerciseAsync(
            string name,
            string instructions,
            ExerciseComplexity complexity)
        {
            var exercise = new Exercise
            {
                Name = name,
                Instructions = instructions,
                ExerciseComplexity = complexity,
            };

            await this.exercisesRepository.AddAsync(exercise);
            await this.exercisesRepository.SaveChangesAsync();
            return exercise.Id;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var query = await this.exercisesRepository
                .All()
                .To<T>()
                .ToListAsync();

            return query;
        }

        public IEnumerable<T> GetAllPaginatedAsync<T>(int? take, int skip)
        {
            var query = this.exercisesRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<int> GetExercisesCountAsync()
        {
            var exercises = this.exercisesRepository
                .All();

            return exercises.Count();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var exercise = await this.exercisesRepository
                .All()
                .Where(d => d.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return exercise;
        }

        public async Task<int> ModifyAsync(
            int id,
            string name,
            string instructions,
            ExerciseComplexity complexity)
        {
            var exercise = await this.exercisesRepository
                .All()
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            exercise.Name = name;
            exercise.Instructions = instructions;
            exercise.ExerciseComplexity = complexity;

            this.exercisesRepository.Update(exercise);
            await this.exercisesRepository.SaveChangesAsync();
            return exercise.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var exercise = await this.exercisesRepository
                .All()
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            this.exercisesRepository.Delete(exercise);
            await this.exercisesRepository.SaveChangesAsync();
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

            var exercises = await this.exercisesRepository
                .All()
                .Where(e => e.ExerciseComplexity == complexity)
                .ToListAsync();

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
                .AllAsNoTracking()
                .Where(h => h.Id == healthDosierId)
                .Select(w => w.WorkoutProgram)
                .FirstAsync();

            return workoutProgramId.Id;
        }

        public async Task<int> GetProgramIdByHealthDosierIdAsync(string healthDosierId)
        {
            var workout = await this.healthDosierRepository
                    .AllAsNoTracking()
                    .Where(h => h.Id == healthDosierId)
                    .Select(w => w.WorkoutProgram.Id)
                    .FirstOrDefaultAsync();

            return workout;
        }

        public async Task DeleteWorkoutProgramAsync(int id)
        {
            var workoutExercise = await this.exercisesWorkoutsRepository
                .AllAsNoTracking()
                .Where(w => w.WorkoutProgramId == id)
                .ToListAsync();

            foreach (var item in workoutExercise)
            {
                this.exercisesWorkoutsRepository.Delete(item);
                await this.exercisesWorkoutsRepository.SaveChangesAsync();
            }

            var workout = await this.workoutRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id);

            this.workoutRepository.Delete(workout);
            await this.workoutRepository.SaveChangesAsync();
        }
    }
}
