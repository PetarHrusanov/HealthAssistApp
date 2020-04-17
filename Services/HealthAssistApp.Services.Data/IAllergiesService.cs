// <copyright file="IAllergiesService.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

using HealthAssistApp.Data.Models.FoodModels;

namespace HealthAssistApp.Services.Data
{
    public interface IAllergiesService
    {
        T ViewByUserId<T>(string userId);

        Allergies GetByUserId (string userId);
    }
}
