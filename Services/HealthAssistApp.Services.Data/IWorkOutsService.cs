// <copyright file="IWorkOutsService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models.Enums;

    public interface IWorkOutsService
    {
        Task<int> CreateWorkoutProgramAsync(ExerciseComplexity complexity, string userId);
    }
}
