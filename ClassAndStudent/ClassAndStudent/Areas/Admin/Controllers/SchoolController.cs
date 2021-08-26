using ClassAndStudent.Areas.Admin.Models;
using ClassAndStudent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAndStudent.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SchoolController : Controller
    {
        
        private readonly ILogger<SchoolController> _logger;
        public SchoolController(ILogger<SchoolController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var model = new CreateBatchModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateBatchModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateBatch();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add batch");
                    _logger.LogError(ex, "Add batch Failed");
                }

            }
            return View(model);
        }
        public IActionResult BatchList()
        {
            var model = new BatchListModel();

            return View(model);
        }
        public JsonResult GetBatchData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new BatchListModel();
            var data = model.GetBatch(dataTableAjaxRequestModel);
            return Json(data);
        }
        public IActionResult Edit(int id)
        {
            var model = new EditBatchModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Edit(EditBatchModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = new BatchListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
