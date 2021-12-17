using HealthChecksBasic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace HealthChecksBasic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HealthCheckService _healthChecksServie;

        public HomeController(ILogger<HomeController> logger,
            HealthCheckService healthChecksServie)
        {
            _logger = logger;
            _healthChecksServie = healthChecksServie;
        }

        public async Task<IActionResult> Index()
        {
            var report = await _healthChecksServie.CheckHealthAsync(
                //(check) => !check.Tags.Contains("error")
                );
            return View(report);
        }

        public async Task<IActionResult> Health()
        {
            var report = await _healthChecksServie.CheckHealthAsync(
                //(check) => !check.Tags.Contains("error")
                );
            return View(report);
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