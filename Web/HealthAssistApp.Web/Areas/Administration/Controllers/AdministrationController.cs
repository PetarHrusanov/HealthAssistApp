namespace HealthAssistApp.Web.Areas.Administration.Controllers
{
    using HealthAssistApp.Common;
    using HealthAssistApp.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
