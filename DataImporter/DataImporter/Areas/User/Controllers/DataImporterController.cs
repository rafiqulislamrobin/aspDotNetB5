using DataImporter.Areas.User.Models;
using DataImporter.Info.Business_Object;
using DataImporter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Controllers
{
    [Area("User")]
    public class DataImporterController : Controller
    {
        private readonly ILogger<DataImporterController> _logger;
      
        public IWebHostEnvironment _WebHostEnvironment;

        public DataImporterController(ILogger<DataImporterController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewGroups()
        {
            return View();
        }
        public IActionResult CreateGroups()
        {
            return View();
        }
        public IActionResult ViewContacts()
        {
            return View();
        }
        public IActionResult CreateContacts()
        {
            return View();
        }

        public IActionResult ImportFile()
        {
            
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> ImportFileAsync(IFormFile file)
        {

            FilePathModel model = new();
            await model.SaveFilePathAsync(file);
            return View();

        }

        public IActionResult ViewImportFile()
        {
            return View();

        }
        public IActionResult ImportHistory()
        {
            var model = new HistoryListModel();

            return View(model);
        }

        public JsonResult GetImportHistoryData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new HistoryListModel();
            var data = model.GetHistories(dataTableAjaxRequestModel);
            return Json(data);
        }

        public IActionResult ExportFile()
        {
            return View();
        }
        public IActionResult ExportFileHistory()
        {
            return View();
        }

    }
}
