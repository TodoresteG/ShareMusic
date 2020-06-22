using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShareMusic.Models;
using ShareMusic.Models.Home;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongsService songsService;

        public HomeController(
            ILogger<HomeController> logger,
            ISongsService songsService)
        {
            _logger = logger;
            this.songsService = songsService;
        }

        public IActionResult Index()
        {
            HomeRecentSongsViewModel viewModel = this.songsService.GetRecentSongs();
            return View(viewModel);
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
