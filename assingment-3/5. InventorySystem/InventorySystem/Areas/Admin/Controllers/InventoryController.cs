using InventorySystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var model = new InventoryModel();
            model.LoadModelData();
            return View(model);
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
    }
}
