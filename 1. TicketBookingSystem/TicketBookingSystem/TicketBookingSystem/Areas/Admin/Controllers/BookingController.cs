using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Areas.Admin.Models;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        public BookingController(ILogger<BookingController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new CustomerListModel();
            
            return View(model);
        }
        public JsonResult GetCustomerData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new CustomerListModel();
            var data = model.GetCustomers(dataTableAjaxRequestModel);
            return Json(data);
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





        public IActionResult Create()
        {
            var model = new CreateCutomerModel();
            return View(model);
        }


        [HttpPost , AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateCutomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateCustomer();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add Customer");
                    _logger.LogError(ex, "Add Customer Failed");
                }
                
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new EditCustomerModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Edit(EditCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = new CustomerListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
