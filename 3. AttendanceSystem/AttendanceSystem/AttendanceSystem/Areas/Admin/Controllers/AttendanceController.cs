using AttendanceSystem.Areas.Admin.Models;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttendanceController : Controller
    {
        private readonly ILogger<AttendanceController> _logger;
        public AttendanceController(ILogger<AttendanceController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new AttendanceModel();
            model.LoadModelData();
            return View(model);
        }
        public JsonResult GetStudentData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new AttendanceModel();
            var data = model.GetStudents(dataTableAjaxRequestModel);
            return Json(data);
        }

        public IActionResult CheckingPresent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckingPresent(AttendanceCheckModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            var model = new CreateStudentModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateStudent();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add Student");
                    _logger.LogError(ex, "Add Student Failed");
                }

            }
            return View(model);
        }
    }
}
