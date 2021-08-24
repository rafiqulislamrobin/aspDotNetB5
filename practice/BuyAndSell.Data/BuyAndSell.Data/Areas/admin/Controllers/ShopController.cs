using BuyAndSell.Data.Areas.admin.Models;
using BuyAndSell.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Areas.admin.Controllers
{
    [Area("Admin")]
    public class ShopController : Controller
    {

        private readonly ILogger<ShopController> _logger;
        public ShopController(ILogger<ShopController> logger)
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
        public IActionResult Create()
        {
            var model = new CreateCutomerModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
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
