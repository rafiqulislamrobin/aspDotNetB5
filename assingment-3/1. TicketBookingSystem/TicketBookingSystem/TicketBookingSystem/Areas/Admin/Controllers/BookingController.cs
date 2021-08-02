using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Areas.Admin.Models;

namespace TicketBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            var model = new BookingModel();
            model.LoadModelData();
            return View(model);
        }

        public IActionResult Booking()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Booking(TicketBookingModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
