namespace HealthAssistApp.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public class Recipes: BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
