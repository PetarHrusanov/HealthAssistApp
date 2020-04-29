namespace HealthAssistApp.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using HealthAssistApp.Data;
    using HealthAssistApp.Services.Data;
    using HealthAssistApp.Web.ViewModels;
    using HealthAssistApp.Web.ViewModels.Administration.AdminitrationTextFilesViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IAdministrationTextService administrationTextService;

        public HomeController(ApplicationDbContext db, IAdministrationTextService administrationTextService)
        {
            this.db = db;
            this.administrationTextService = administrationTextService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Privacy()
        {

            var file = await this.administrationTextService.GetByNameAsync<AdminTextFilesDetailsViewModels>("Privacy Policy");
            return this.View(file);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
