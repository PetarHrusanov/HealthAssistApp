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

        IEnumerable<T> GetAll<T>(int? count = null);

        Task<T> GetByIdAsync<T>(int id);

        Task<int> ModifyAsync(
            int id,
            string name,
            string instructions,
            ExerciseComplexity complexity);

        Task DeleteByIdAsync(int id);

        Task<int> CreateWorkoutProgramAsync(ExerciseComplexity complexity, string userId);

        public Task<int> GetWorkoutProgramsByHealthDosierId(string healthDosierId);

        IEnumerable<T> GetExercisesByWorkoutId<T>(int workoutprogramId, int? take = null, int skip = 0);

        public Task<int> GetExercisesCountByWorkoutId(int workoutId);
    }
}
