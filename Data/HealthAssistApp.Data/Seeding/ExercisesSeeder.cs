// <copyright file="ExercisesSeeder.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using HealthAssistApp.Data.Models.Enums;

    public class ExercisesSeeder : ISeeder
    {
        public ExercisesSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Exercises != null)
            {
                return;
            }

            var exercises = new[]
            {
                (Name: "10 Sit-ups",
                Description: "Lay on the ground, waist on the ground at all time. Gently lift only the upper body",
                Complexity: ExerciseComplexity.Low),
                (Name: "Jogging",
                Description: "Run slowly for 1 km, watch your breath and monitor your heartbeat.",
                Complexity: ExerciseComplexity.Low),
                (Name: "Jumps",
                Description: "Jump 20 times in four sets.",
                Complexity: ExerciseComplexity.Low),
                (Name: "Run",
                Description: "Run for 2 km, watch your breath and monitor your heartbeat.",
                Complexity: ExerciseComplexity.Medium),
                (Name: "Push-ups",
                Description: "Push yourself from the ground 10 times in four sets. Keep your back straight.",
                Complexity: ExerciseComplexity.Medium),
                (Name: "20 Sit-ups",
                Description: "Lay on the ground, waist on the ground at all time. Gently lift only the upper body. Do 4 repetitions.",
                Complexity: ExerciseComplexity.Medium),
                (Name: "Burpees",
                Description: "Jump with a deep lounge. As you land, get on the ground and do a push-up. Do 4 sets of 6 repetitions.",
                Complexity: ExerciseComplexity.Hard),
            };
            foreach (var exercise in exercises)
            {
                await dbContext.Exercises.AddAsync(new Models.WorkingOut.Exercise
                {
                    Name = exercise.Name,
                    Instructions = exercise.Description,
                    ExerciseComplexity = exercise.Complexity,
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
