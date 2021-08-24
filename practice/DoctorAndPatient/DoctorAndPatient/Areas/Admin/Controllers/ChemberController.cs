using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAndPatient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChemberController : Controller
    {
        private readonly ILogger<ChemberController> _logger;
        public ChemberController(ILogger<ChemberController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
