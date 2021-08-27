using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAndAuthor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger;
        public LibraryController(ILogger<LibraryController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
