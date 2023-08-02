using NosiYa.Common;
using NosiYa.Services.Messaging;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace NosiYa.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Home;

    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        } TODO use or delete*/

        /*private readonly SendGridEmailSender emailSender;

        public HomeController(IConfiguration configuration)
        {
            emailSender = new SendGridEmailSender(configuration["SendGridApiKey"]);
        }*/

        public IActionResult Index()
        {
            // this.emailSender.SendEmailAsync("gnt@abv.bg", "Hello world!", "<p>Wellcome to NosiYa!</p>"); //await ??
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}