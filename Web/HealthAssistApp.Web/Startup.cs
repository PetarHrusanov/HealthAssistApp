// <copyright file="Startup.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web
{
    using System.Reflection;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Common;
    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Repositories;
    using HealthAssistApp.Data.Seeding;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Services.Data.BodySystems;
    using HealthAssistApp.Services.Mapping;
    using HealthAssistApp.Services.Messaging;
    using HealthAssistApp.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Recipes.Services.Data;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            services.AddTransient<IAdministrationTextService, AdministrationTextService>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();

            // Food-related Services
            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<IAllergiesService, AllergiesService>();
            services.AddTransient<IFoodRegimensService, FoodRegimensService>();

            // Health Parameters Service
            services.AddTransient<IHealthParametersService, HealthParametersService>();

            // Disease-related Services
            services.AddTransient<IDiseasesService, DiseasesService>();
            services.AddTransient<ISymptomsServices, SymptomsService>();
            services.AddTransient<IBodySystemsService, BodySystemsService>();

            // HealthDosier-related Services
            services.AddTransient<IHealthDosiersService, HealthDosiersService>();

            // Working Out Service
            services.AddTransient<IWorkOutsService, WorkOutsService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("recipesDetails", "Recipes/{name:minlength(3)}", new { controller = "Recipes", action = "ByName" });
                        endpoints.MapControllerRoute("diseasesDetails", "Diseases/{name:minlength(3)}", new { controller = "Diseases", action = "ByName" });
                        endpoints.MapControllerRoute("userAllergies", "Allergies/{userId:minlength(3)}", new { controller = "Allergies", action = "ByUserId" });
                        endpoints.MapControllerRoute("userAllergiesModify", "Allergies/Modify/{ApplicationUserId:minlength(3)}", new { controller = "Allergies", action = "Modify" });
                        endpoints.MapControllerRoute("userHealthParamModify", "HealthParameters/Modify/{userId:minlength(3)}", new { controller = "HealthParameters", action = "Modify" });
                        endpoints.MapControllerRoute("userFoodRegimen", "FoodRegimens/{healthDosierId}", new { controller = "FoodRegimens", action = "ByHealthDosier" });
                        endpoints.MapControllerRoute("userWorkoutProgram", "WorkoutPrograms/{healthDosierId}", new { controller = "Workouts", action = "ByHealthDosier" });
                        //endpoints.MapControllerRoute("adminDiseaseSymptom", "Administration/DiseasesSymptoms/Delete/{idS}", new { controller = "DiseasesSymptoms", action = "Delete" });
                        endpoints.MapRazorPages();
                    });
        }
    }
}
