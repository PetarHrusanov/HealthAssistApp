// <copyright file="DiseaseLogicSeeder.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DiseaseLogicSeeder : ISeeder
    {
        public DiseaseLogicSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BodySystems.Any())
            {
                return;
            }

            // Body Systems
            var bodySystems = new List<string>
            {
                "Respiratory System",
                "Excretory System",
                "Circulatory System",
                "Digestive System",
                "Muscular System",
                "Nervous System",

            };

            foreach (var bodySystem in bodySystems)
            {
                await dbContext.BodySystems.AddAsync(new Models.BodySystem
                {
                    Name = bodySystem,
                });

                await dbContext.SaveChangesAsync();
            }

            // Symptoms For Respiratory
            var respiratorySymptoms = new List<string>
            {
                "Dry Cough",
                "Sore throat",
                "Pain in the chest",
                "Dry mouth",
                "Whipping cough",
                "Breathing noisily",
                "Difficulty breathing",
            };

            var respiratorySystemId = dbContext.BodySystems
                .Where(b => b.Name == "Respiratory System")
                .Select(i => i.Id)
                .FirstOrDefault();

            foreach (var symptom in respiratorySymptoms)
            {
                await dbContext.Symptoms.AddAsync(new Models.Symptom
                {
                    Description = symptom,
                    BodySystemId = respiratorySystemId,
                });

                await dbContext.SaveChangesAsync();
            }

            // Symptoms
            if (dbContext.Symptoms.Any())
            {
                return;
            }

            // Symptoms For Excretory
            var excretorySymptoms = new List<string>
            {
                "Pain in the kidneys",
                "Painful urination",
                "Regular urination",
                "Cloudy urine",
                "Irregular urine smell",
                "Blood in the urine",

            };

            var excretorySystemId = dbContext.BodySystems
                .Where(b => b.Name == "Excretory System")
                .Select(i => i.Id)
                .FirstOrDefault();

            foreach (var symptom in excretorySymptoms)
            {
                await dbContext.Symptoms.AddAsync(new Models.Symptom
                {
                    Description = symptom,
                    BodySystemId = excretorySystemId,
                });
            }

            // Symptoms for Circulatory
            var circulatorySymptoms = new List<string>
            {
                "Cold limbs",
                "Irregular heartbeat",
                "Pain in the heart",

            };

            var circulatorySystemId = dbContext.BodySystems
                .Where(b => b.Name == "Circulatory System")
                .Select(i => i.Id)
                .FirstOrDefault();

            foreach (var symptom in circulatorySymptoms)
            {
                await dbContext.Symptoms.AddAsync(new Models.Symptom
                {
                    Description = symptom,
                    BodySystemId = circulatorySystemId,
                });

                await dbContext.SaveChangesAsync();
            }

            // Symptoms Digestive System
            var digestiveSymptoms = new List<string>
            {
                "Constipation",
                "Stomach ache",
                "Bad breath",
            };

            var digestiveSystemId = dbContext.BodySystems
                .Where(b => b.Name == "Digestive System")
                .Select(i => i.Id)
                .FirstOrDefault();

            foreach (var symptom in digestiveSymptoms)
            {
                await dbContext.Symptoms.AddAsync(new Models.Symptom
                {
                    Description = symptom,
                    BodySystemId = digestiveSystemId,
                });

                await dbContext.SaveChangesAsync();
            }

            // Symptoms for Muscular
            var muscularSymptoms = new List<string>
            {
                "Muscle spasms",
                "Teeth grinding",
            };

            var muscleSystemId = dbContext.BodySystems
                .Where(b => b.Name == "Muscular System")
                .Select(i => i.Id)
                .FirstOrDefault();

            foreach (var symptom in muscularSymptoms)
            {
                await dbContext.Symptoms.AddAsync(new Models.Symptom
                {
                    Description = symptom,
                    BodySystemId = muscleSystemId,
                });

                await dbContext.SaveChangesAsync();
            }

            // Symptoms for Nervous
            var nervousSymptoms = new List<string>
            {
                "Involuntairly eyelid movement",
                "Tingling in the hands",
                "Pain in the spine",
            };

            var nervousSystemId = dbContext.BodySystems
                .Where(b => b.Name == "Nervous System")
                .Select(i => i.Id)
                .FirstOrDefault();

            foreach (var symptom in nervousSymptoms)
            {
                await dbContext.Symptoms.AddAsync(new Models.Symptom
                {
                    Description = symptom,
                    BodySystemId = nervousSystemId,
                });

                await dbContext.SaveChangesAsync();
            }

            // Diseases
            if (dbContext.Diseases.Any())
            {
                return;
            }

            var diseases = new[]
            {
                (Name: "COVID-19",
                Description: "Initially it starts with a temperature, which later develops to a whipping cough.",
                Advice: "Stay isolated for 14 days, in case of prolonged fever, seek medical attention"),
                (Name: "High cholesterol",
                Description: "High cholesterol level might increase your risk or heart attack, etc.",
                Advice: "Reduce the consumption of meat with fats and hydrogenated fats as well."),
                (Name: "Digestive enzyme deficiency",
                Description: "The lack of digestive enzymes reduces the ability of the organism to absorb nutrients.",
                Advice: "Increase the consumption of fresh vegetables, adding Enzy-Mill might help."),
            };

            foreach (var disease in diseases)
            {
                await dbContext.Diseases.AddAsync(new Models.Disease
                {
                    Name = disease.Name,
                    Description = disease.Description,
                    Advice = disease.Advice,
                });

                await dbContext.SaveChangesAsync();
            }

            if (dbContext.DiseaseSymptoms.Any())
            {
                return;
            }

            // COVID-19 Symptoms
            var covid = await dbContext.Diseases
                .Where(d => d.Name == "COVID-19")
                .Select(i => i.Id)
                .FirstOrDefaultAsync();

            var covidSymptoms = await dbContext.Symptoms
                .Where(s => s.BodySystem.Name == "Respiratory System")
                .Select(i => i.Id)
                .ToListAsync();

            foreach (var symptom in covidSymptoms)
            {
                await dbContext.DiseaseSymptoms.AddAsync(new Models.DiseaseSymptom
                {
                    DiseaseId = covid,
                    SymptomId = symptom,
                });

                await dbContext.SaveChangesAsync();
            }

            // Enzyme Deficiency Symptoms
            var enzymeId = await dbContext.Diseases
                .Where(d => d.Name == "Digestive enzyme deficiency")
                .Select(i => i.Id)
                .FirstOrDefaultAsync();

            var enzymeSymptoms = await dbContext.Symptoms
                .Where(s => s.BodySystem.Name == "Digestive System")
                .Select(i => i.Id)
                .ToListAsync();

            foreach (var symptom in enzymeSymptoms)
            {
                await dbContext.DiseaseSymptoms.AddAsync(new Models.DiseaseSymptom
                {
                    DiseaseId = enzymeId,
                    SymptomId = symptom,
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
