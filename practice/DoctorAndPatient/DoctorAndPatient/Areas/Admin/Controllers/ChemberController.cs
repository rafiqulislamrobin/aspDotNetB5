using DoctorAndPatient.Areas.Admin.Models;
using DoctorAndPatient.Models;
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
        public IActionResult DoctorList()
        {
            var model = new DoctorListModel();

            return View(model);
        }
        public JsonResult GetdoctorData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new DoctorListModel();
            var data = model.GetDoctors(dataTableAjaxRequestModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            var model = new CreateDoctorModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateDoctorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateDoctor();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add Doctor");
                    _logger.LogError(ex, "Add Doctor Failed");
                }

            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var model = new EditDoctorModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Edit(EditDoctorModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = new DoctorListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
