// <copyright file="BaseServicesTest.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Services.Data.Tests
{
    using System;

    using HealthAssistApp.Data;
    using HealthAssistApp.Data.Common;
    using HealthAssistApp.Data.Common.Repositories;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Repositories;
    using HealthAssistApp.Services.Data.BodySystems;
    using HealthAssistApp.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Recipes.Services.Data;
    using Xunit;

    public abstract class BaseServicesTests
    {
        protected BaseServicesTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        //public void Dispose()
        //{
        //    this.DbContext.Database.EnsureDeleted();
        //    this.SetServices();
        //}

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            // da si mock-na User-a vmesto da puskam tova
            //services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
            //    .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Services
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient(typeof(ILoggerFactory), typeof(LoggerFactory));
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
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

            //// AutoMapper
            //AutoMapperConfig.RegisterMappings(typeof(EventListViewModel).GetTypeInfo().Assembly);

            return services;
        }
    }
}
