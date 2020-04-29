namespace HealthAssistApp.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Common.Models;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.DiseaseModels;
    using HealthAssistApp.Data.Models.FoodModels;
    using HealthAssistApp.Data.Models.WorkingOut;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<TextFilesForAdministration> TextFilesForAdministration { get; set; }

        // Disease Related Objects
        public DbSet<Disease> Diseases { get; set; }

        public DbSet<BodySystem> BodySystems { get; set; }

        public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }

        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<HealthDosierDisease> HealthDosierDiseases { get; set; }

        public DbSet<UserSymptoms> UserSymptoms { get; set; }

        // Exercise Related Objects
        public DbSet<Exercise> Exercises { get; set;}

        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }

        public DbSet<ExerciseWorkoutProgram> ExerciseWorkoutPrograms { get; set; }

        public DbSet<HealthDosier> HealthDosiers { get; set; }

        public DbSet<HealthParameters> HealthParameters { get; set; }

        public DbSet<Allergies> Allergies { get; set; }

        // Food Related Objects
        public DbSet<FoodRegimen> FoodRegimens { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Meal> Meals { get; set;}

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeIngredients> RecipeIngredients { get; set; }

        // General Logic
        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<RecipeIngredients>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId });

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK_Recipe_RecipeIngredients");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .HasConstraintName("FK_Ingredient_RecipeIngredients");
            });

            builder.Entity<ExerciseWorkoutProgram>(entity =>
            {
                entity.HasKey(e => new { e.ExerciseId, e.WorkoutProgramId });

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.ExerciseWorkoutPrograms)
                    .HasForeignKey(d => d.ExerciseId)
                    .HasConstraintName("FK_Exercise_ExrciseWorkoutProgram");

                entity.HasOne(d => d.WorkoutProgram)
                    .WithMany(p => p.ExerciseWorkoutPrograms)
                    .HasForeignKey(d => d.WorkoutProgramId)
                    .HasConstraintName("FK_WorkoutProgram_ExerciseWorkoutProgram");
            });

            builder.Entity<DiseaseSymptom>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SymptomId });

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.DiseaseSymptoms)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Disease_DiseaseSymptoms");

                entity.HasOne(d => d.Symptom)
                    .WithMany(p => p.DiseaseSymptoms)
                    .HasForeignKey(d => d.SymptomId)
                    .HasConstraintName("FK_Symptom_DiseaseSymptoms");
            });

            builder.Entity<HealthDosierDisease>(entity =>
            {
                entity.HasKey(e => new { e.HealthDosierId, e.DiseaseId });

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.HealthDosierDiseases)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Disease_HealthDosierDisease");

                entity.HasOne(d => d.HealthDosier)
                    .WithMany(p => p.HealthDosierDiseases)
                    .HasForeignKey(d => d.HealthDosierId)
                    .HasConstraintName("FK_Health_HealthDosierDisease");
            });

            builder.Entity<Meal>(entity =>
            {
                entity.HasOne(m => m.Breakfast)
                    .WithMany(r => r.Breakfasts)
                    .HasForeignKey(d => d.BreakfastId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Lunch)
                    .WithMany(r => r.Lunches)
                    .HasForeignKey(d => d.LunchId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Diner)
                    .WithMany(r => r.Diners)
                    .HasForeignKey(d => d.DinerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
