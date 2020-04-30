// <copyright file="IWorkOutsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models.Enums;

    public interface IWorkOutsService
    {
        Task<int> CreateExerciseAsync(string name, string instructions, ExerciseComplexity complexity);

        Task<IEnumerable<T>> GetAll<T>();

        IEnumerable<T> GetAllPaginatedAsync<T>(int? take = null, int skip = 0);

        Task<int> GetExercisesCountAsync();

        Task<T> GetByIdAsync<T>(int id);

        Task<int> ModifyAsync(
            int id,
            string name,
            string instructions,
            ExerciseComplexity complexity);

        Task DeleteByIdAsync(int id);

        Task<int> CreateWorkoutProgramAsync(ExerciseComplexity complexity, string userId);

        Task<int> GetProgramIdByHealthDosierIdAsync(string healthDosierId);

        Task DeleteWorkoutProgramAsync(int id);

        public Task<int> GetWorkoutProgramsByHealthDosierId(string healthDosierId);

        IEnumerable<T> GetExercisesByWorkoutId<T>(int workoutprogramId, int? take = null, int skip = 0);

        public Task<int> GetExercisesCountByWorkoutId(int workoutId);
    }
}
