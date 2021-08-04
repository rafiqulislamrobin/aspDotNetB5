using InventorySystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventorySystem.Models;

namespace InventorySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new ProductListModel();
            model.LoadModelData();
            return View(model);
        }
        public JsonResult GetProductData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new ProductListModel();
            var data = model.GetProducts(dataTableAjaxRequestModel);
            return Json(data);
        }


        public IActionResult StockChecking()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StockChecking(ProductCheckModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            var model = new CreateProductModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateProduct();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add Product");
                    _logger.LogError(ex, "Add Product Failed");
                }

            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var model = new EditProductModel();
            model.LoadModelData(id);
            return View(model);

        }


        [HttpPost, AutoValidateAntiforgeryToken]

        public IActionResult Edit(EditProductModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = new ProductListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
