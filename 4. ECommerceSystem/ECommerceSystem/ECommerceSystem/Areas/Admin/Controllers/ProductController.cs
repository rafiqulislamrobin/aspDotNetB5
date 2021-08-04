using ECommerceSystem.Areas.Admin.Models;
using ECommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
       

        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            var model = new ProductModel();
            model.LoadModelData();
            return View(model);
        }
        public JsonResult GetAllProductData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new ProductModel();
            var data = model.GetAllProductData(dataTableAjaxRequestModel);
            return Json(data);
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
            var model = new ProductModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
