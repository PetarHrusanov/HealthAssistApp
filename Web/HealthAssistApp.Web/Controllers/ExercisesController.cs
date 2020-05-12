// <copyright file="ExercisesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using DinkToPdf;
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

            // parvata opciq za pdf creation 
            //var fileContents = this.htmlToPdfConverter.Convert(this.environment.ContentRootPath, htmlData);
            //return this.File(fileContents, "application/pdf");

            //// vtorata opciq 
            //string currentDirectory = Directory.GetCurrentDirectory();
            //string phantomJsRootFolder = Path.Combine(currentDirectory, "Methods", "PDFNewVersion", "PhantomJsRoot");
            //string dedicatedDirectory = Path.Combine(currentDirectory, "Controllers");

            //// the pdf generator needs to know the path to the folder with .exe files.
            //PdfGenerator generator = new PdfGenerator(phantomJsRootFolder);

            //string htmlToConvert = htmlData;

            //// Generate pdf from html and place in the current folder.
            //var pathOftheGeneratedPdf = generator.GeneratePdf(htmlToConvert, dedicatedDirectory);

            //// tuk svarshva vtorata opciq
            ///

            var converter = new SynchronizedConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },

                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HtmlContent = htmlData,
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                    },
                },
            };

            byte[] pdf = converter.Convert(doc);

            return File(pdf, "application/pdf");

            //return RedirectToAction("Index");
        }
    }
}
