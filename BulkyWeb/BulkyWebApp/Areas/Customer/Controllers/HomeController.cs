using System.Diagnostics;
using Bulky.Models;
using DI_Service_Lifetime;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedGuidService _scoped1;
        private readonly IScopedGuidService _scoped2;

        private readonly ITransientGuidService _transient1;
        private readonly ITransientGuidService _transient2;

        private readonly ISingletonGuidService _singleton1;
        private readonly ISingletonGuidService _singleton2;

        public HomeController(ILogger<HomeController> logger,
            IScopedGuidService scoped1,
            IScopedGuidService scoped2,
            ITransientGuidService transient1,
            ITransientGuidService transient2,
            ISingletonGuidService singleton1,
            ISingletonGuidService singleton2)
        {
            _logger = logger;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _transient1 = transient1;
            _transient2 = transient2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"Scoped 1 : {_scoped1.GetGuid()}");
            _logger.LogInformation($"Scoped 2 : {_scoped2.GetGuid()}");
            _logger.LogInformation($"Transient 1 : {_transient1.GetGuid()}");
            _logger.LogInformation($"Transient 2 : {_transient2.GetGuid()}");
            _logger.LogInformation($"Singleton 1 : {_singleton1.GetGuid()}");
            _logger.LogInformation($"Singleton 2 : {_singleton2.GetGuid()}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
