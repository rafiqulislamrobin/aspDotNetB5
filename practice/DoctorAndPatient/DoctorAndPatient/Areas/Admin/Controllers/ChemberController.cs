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
    }
}
