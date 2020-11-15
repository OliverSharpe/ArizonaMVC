using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Arizona.Models;
using Arizona.ViewModels;
using DutchTreat.Services;
using Arizona.Data;
using Microsoft.AspNetCore.Authorization;

namespace Arizona.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;
        private readonly ArizonaContext _arizonaContext;
        private readonly IArizonaRepository _arizonaRepository;

        public HomeController(ILogger<HomeController> logger, IMailService mailService, ArizonaContext arizonaContext, IArizonaRepository arizonaRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _arizonaContext = arizonaContext;
            _arizonaRepository = arizonaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var products = _arizonaRepository.GetAllProducts();
            return View(products);
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("testemail@testing.com", model.Subject, $"Message from {model.Name} at {model.Email} : {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
