namespace MDManagement.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ManagementController : Controller
    {
        public ManagementController()
        {

        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
