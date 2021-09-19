using DataImporter.Areas.User.Models;
using DataImporter.Info.Business_Object;
using DataImporter.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Controllers
{
    [Area("User")]
    public class DataImporterController : Controller
    {
        private readonly ILogger<DataImporterController> _logger;

        public IWebHostEnvironment _WebHostEnvironment;
        public int temp { get; set; }
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
            var model = new ViewGroupModel();
            return View(model);
        }
        public JsonResult GetGroupsData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new ViewGroupModel();
            var data = model.GetGroups(dataTableAjaxRequestModel);
            return Json(data);
        }
        public IActionResult CreateGroups()
        {
            var model = new CreateGroupModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateGroups(CreateGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateGroup();
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("", "Failed to Create Group");
                    if (ex.Message == "Group name is already used")
                    {
                        ViewBag.DuplicationMessage = "Group name already used , try another name";
                    }
                    _logger.LogError(ex, "Add Group Failed");
                }

            }
            return View(model);
        }
        public IActionResult EditGroup(int id)
        {
            var model = new EditGroupModel();
            model.LoadModelData(id);
            return View(model);

        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult EditGroup(EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("", "Failed to Create Group");
                    if (ex.Message == "Group name is already used")
                    {
                        ViewBag.DuplicationMessage = "Group name already used , try another name";
                    }
                    _logger.LogError(ex, "Upload Group Failed");
                    return View(model);
                }
            }
            return View(nameof(ViewGroups));
        }
        public IActionResult DeleteGroup(int id)
        {
            var model = new CreateGroupModel();
            model.DeleteGroup(id);
            return RedirectToAction(nameof(ViewGroups));
        }
        [HttpGet]
        public IActionResult ViewContacts()
        {
            var model = new ViewContactModel();
            var z = model.GetContactList();
            return View(z);
        }
        [HttpPost]

        public IActionResult ViewContacts(IFormFile file, FilePathModel models)
        {
            //var model = new ViewContactModel();
            //var z = model.GetContactList();
            //convert to a stream

            var filepath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Excel"}" + "\\" + file.FileName;
            using (FileStream fileStream = System.IO.File.Create(filepath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            ConfirmFile model = new ConfirmFile();
             model.ConfirmFileUpload(filepath);

            TempData["file"] = Path.GetFileName(filepath);
            TempData["temp"] = models.GroupId;
            return View(model);

        }
        public IActionResult CreateContacts()
        {
            var model = new CreateContactModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateContacts(CreateContactModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateContact();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Create Contact");
                    _logger.LogError(ex, "Add Contact Failed");
                }

            }
            return View(model);
        }

        public IActionResult ImportFile()
        {
            var model = new FilePathModel();
            var list = model.LoadAllGroups();
            ViewBag.GroupList = new SelectList(list, "Id", "Name");
            return View(model);

        }

        //[HttpPost]

        public IActionResult UploadHistory()
        {
            var filename = TempData["file"].ToString();
            var groupId = Convert.ToInt32(TempData["temp"]);
            var model = new FilePathModel();
            var list = model.LoadAllGroups();


            /* await*/
            model.SaveFilePathAsync(filename, groupId, list);
            return View(nameof(ImportHistory));

        }


        public IActionResult ViewImportFile()
        {
            return View();

        }
        public IActionResult ImportHistory()
        {

            return View();
        }

        public JsonResult GetImportHistoryData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new HistoryListModel();
            var data = model.GetHistories(dataTableAjaxRequestModel);
            return Json(data);
        }


        public IActionResult ExportFileHistory()
        {
            return View();
        }
        public IActionResult ExportFile()
        {

            var model = new ExportFileModel();
            model.GetContactsList();
            return View(model);
        }
        public IActionResult DownloadFile()
        {
            var model = new ExportFileModel();
            var contacts = model.GetExportFiles();
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileFormat = "User.xlsx";
            return File(contacts, fileType, fileFormat);
        }

    }
}
