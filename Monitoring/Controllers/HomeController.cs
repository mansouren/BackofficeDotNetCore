using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Monitoring.Hubs;
using Repository.Interfaces.B2STxnInterfaceRepositories;

namespace Monitoring.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
          
            return View();
        }

    }
}