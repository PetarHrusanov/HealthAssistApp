namespace HealthAssistApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class HealthAssistAppSeeder : ISeeder
    {
        public HealthAssistAppSeeder()
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
            }

            // Symptoms Digestive System
            var digestiveSymptoms = new List<string>
            {
                "Cold limbs",
                "Irregular heartbeat",
                "Pain in the heart",
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
            }

            // Symptoms for Muscular
            var muscularSymptoms = new List<string>
            {
                "Muscle spasms",
                "Irregular heartbeat",
                "Pain in the heart",
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
            }

            // Symptoms for Nervous
            var nervousSymptoms = new List<string>
            {
                "Muscle spasms",
                "Irregular heartbeat",
                "Pain in the heart",
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
            }

            // Recipes
            if (dbContext.Recipes.Any())
            {
                return;
            }

            //var 
        }
    }
}
