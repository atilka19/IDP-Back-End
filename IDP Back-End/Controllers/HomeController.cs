using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;

namespace IDP_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _repo;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository _catRepo)
        {
            _logger = logger;
            _repo = _catRepo;
        }

        public IActionResult Index()
        {
            var categories = _repo.GetAllCategorries();
            return View(categories);
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
