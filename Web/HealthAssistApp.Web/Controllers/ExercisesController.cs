// <copyright file="ExercisesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.Methods;
    using HealthAssistApp.Web.Methods.PDF;
    using HealthAssistApp.Web.ViewModels.Exercises;
    using HealthAssistApp.Web.ViewModels.Workouts;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using PhantomJs.NetCore;

    public class ExercisesController : BaseController
    {
        private readonly IWorkOutsService workOutsService;
        private const int ItemsPerPage = 5;
        private readonly IViewRenderService viewRenderService;
        private readonly IHtmlToPdfConverter htmlToPdfConverter;
        private readonly IHostingEnvironment environment;
        private readonly object RenderViewAsync;

        public ExercisesController(
            IWorkOutsService workOutsService,
            IViewRenderService viewRenderService,
            IHtmlToPdfConverter htmlToPdfConverter,
            IHostingEnvironment environment)
        {
            this.workOutsService = workOutsService;
            this.viewRenderService = viewRenderService;
            this.htmlToPdfConverter = htmlToPdfConverter;
            this.environment = environment;
        }

        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            var viewModel = new ExercisesIndexViewModel
            {
                Exercises = this.workOutsService
                .GetAllPaginatedAsync<ExercisesWorkoutModel>(ItemsPerPage, (page - 1) * ItemsPerPage)
                as ICollection<ExercisesWorkoutModel>,
            };

            var count = await this.workOutsService.GetExercisesCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> AllExercisesAsync()
        {
            var viewModel = new ExercisesAllViewModel
            {
                Exercises = await this.workOutsService
                .GetAll<ExercisesWorkoutModel>()
                as ICollection<ExercisesWorkoutModel>,
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPdf(int page = 1)
        {
            var viewModel = new ExercisesAllViewModel
            {
                Exercises = await this.workOutsService
                .GetAll<ExercisesWorkoutModel>()
                as ICollection<ExercisesWorkoutModel>,
            };

            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Exercises/AllExercises.cshtml", viewModel);
            //var fileContents = this.htmlToPdfConverter.Convert(this.environment.ContentRootPath, htmlData);
            //return this.File(fileContents, "application/pdf");

            string currentDirectory = Directory.GetCurrentDirectory();
            string phantomJsRootFolder = Path.Combine(currentDirectory, "Methods", "PDFNewVersion", "PhantomJsRoot");

            // the pdf generator needs to know the path to the folder with .exe files.
            PdfGenerator generator = new PdfGenerator(phantomJsRootFolder);

            string htmlToConvert = htmlData;

            // Generate pdf from html and place in the current folder.
            string pathOftheGeneratedPdf = generator.GeneratePdf(htmlToConvert, currentDirectory);

            //Console.WriteLine("Pdf generated at: " + pathOftheGeneratedPdf);

            return RedirectToAction("Index");
        }
    }
}
