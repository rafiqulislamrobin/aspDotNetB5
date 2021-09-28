using Autofac;
using DataImporter.Areas.User.Models;
using DataImporter.Info.Business_Object;
using DataImporter.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
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
    [Area("User"), Authorize(Policy = "RestrictedArea")]
    public class DataImporterController : Controller
    {
        private readonly ILogger<DataImporterController> _logger;
        private readonly ILifetimeScope _scope;
        public IWebHostEnvironment _WebHostEnvironment;
        public int temp { get; set; }
        public DataImporterController(ILogger<DataImporterController> logger , ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }
       
        public IActionResult Index()
        {
            IndexModel model = new IndexModel();
            model.GetTotal();
            return View(model);
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




        public IActionResult ImportFile()
        {
            var model = new FilePathModel();
            var list = model.LoadAllGroups();
            ViewBag.GroupList = new SelectList(list, "Id", "Name");
            return View(model);

        }
        [HttpPost]
        public IActionResult ImportFile(IFormFile file, FilePathModel filepathmodel)
        {
            var model = new FilePathModel();
            model.GroupId = filepathmodel.GroupId;
            
            //convert to a stream
            var filepath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\ExcelFiles"}" + "\\" + file.FileName;
            using (FileStream fileStream = System.IO.File.Create(filepath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var path = Path.GetFileName(filepath);
            model.file = path;
            return RedirectToAction("ConfirmContacts", model);

        }
        public IActionResult ConfirmContacts(FilePathModel filemodels)
        {
            ConfirmFile model = new ConfirmFile();
            model.GroupId = filemodels.GroupId;
            model.file = filemodels.file;
            var file = filemodels.file;
            var filepath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\ExcelFiles"}" + "\\" + file;

           var z = model.ConfirmFileUpload(filepath);
            if (z.Item2==null)
            {
                ViewBag.HeaderMissMatch = "FIles Columns dosent match to this group." +
                                          " Please select another group or create a group";
                System.IO.File.Delete(filepath);
                var list = filemodels.LoadAllGroups();
                ViewBag.GroupList = new SelectList(list, "Id", "Name");
                return View(nameof(ImportFile));
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult ConfirmContacts(ConfirmFile model)
        {

            var FilePathModels = new FilePathModel();
            var list = FilePathModels.LoadAllGroups();

            FilePathModels.SaveFilePath(model.file, model.GroupId, list);
            return RedirectToAction(nameof(ImportHistory));

        }
        public IActionResult CancelImportFile(ConfirmFile ConfirmModel)
        {
            var model = new FilePathModel();
            var list = model.LoadAllGroups();
            ViewBag.GroupList = new SelectList(list, "Id", "Name");
            model.CancelImport(ConfirmModel.file);
            return View(nameof(ImportFile));
        }


        public IActionResult ImportHistory(ImportHistoryModel importHistoryModel)
        {
            TempData["DateTo"] = importHistoryModel.DateTo;
            TempData["DateFrom"] = importHistoryModel.DateFrom;
            return View();
           

        }
        public JsonResult GetImportHistoryData()
        {
            
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new ImportHistoryModel();
            model.DateTo = Convert.ToDateTime(TempData["DateTo"]);
            model.DateFrom = Convert.ToDateTime(TempData["DateFrom"]);
            var data = model.GetHistories(dataTableAjaxRequestModel);
            return Json(data);
        }
       
        public IActionResult ViewContacts()
        {
            var model = new ExportFileModel();

            var list = model.LoadAllGroups();
            ViewBag.GroupList = new SelectList(list, "Id", "Name");

            return View(model);
        }
        [HttpPost]
        public IActionResult ViewContacts(FilePathModel filePathmodel)
        {
            var model = new ExportFileModel();
            model.GetContactsList(filePathmodel.GroupId);

            var list = model.LoadAllGroups();
            if (model.Headers.Count == 0)
            {
                ViewBag.ContactListNullMassage = "(No Contact Available)";
                ViewBag.GroupList = new SelectList(list, "Id", "Name");
            }
            else
            {
                ViewBag.GroupList = new SelectList(list, "Id", "Name");
            }

            List<string> headers = new();
            foreach (var item in model.Headers)
            {
                headers.Add(item);
            }

            TempData["id"] = filePathmodel.GroupId;

            return View(model);
        }
        [HttpGet]
        public IActionResult ExportFile()
        {

            var model = new ExportFileModel();
          
            var list = model.LoadAllGroups();
            ViewBag.GroupList = new SelectList(list, "Id", "Name");
        
            return View(model);
            
        }
        [HttpPost]
        public IActionResult ExportFile(FilePathModel filePathmodel)
        {
            var model = new ExportFileModel();
            model.GetContactsList(filePathmodel.GroupId);
            
            var list = model.LoadAllGroups();
            if (model.Headers.Count == 0)
            {
                ViewBag.ContactListNullMassage = "(No Contact Available)";
                ViewBag.GroupList = new SelectList(list, "Id", "Name");
            }
            else
            { 
                ViewBag.GroupList = new SelectList(list, "Id", "Name");
            }

            List<string> headers = new();
            foreach (var item in model.Headers)
            {
                headers.Add(item);
            }

            TempData["id"] = filePathmodel.GroupId;

            return View(model);

        }
      
        public IActionResult Download()
        {
            var id = Convert.ToInt32(TempData.Peek("id"));

            var model = new ExportFileModel();
            model.GetContactsList(id);
            var contacts = model.GetExportFiles();
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileFormat = "User.xlsx";
            return File(contacts, fileType, fileFormat);
        }
        public IActionResult DownloadFromExportHistory(int id)
        {
            var model = new ExportFileModel();
            model.GetExportFileHistory(id);
            model.GetContactsListByDate(model.GroupId) ;
            var contacts = model.GetExportFiles();
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileFormat = "User.xlsx";
            return File(contacts, fileType, fileFormat);
        }
        public IActionResult EmailSender()
        {
            EmailSenderModel model = new();
            model.GroupId = Convert.ToInt32(TempData.Peek("id"));
            return View(model);
        }
        [HttpPost]
        public IActionResult EmailSender(EmailSenderModel emailSenderModel)
        {
            var groupId = emailSenderModel.GroupId;
            var email = (emailSenderModel.Email);


            emailSenderModel.GetData(groupId);
            emailSenderModel.SendEmail(email);

            ExportStatusModel model = new ExportStatusModel();
            model.MakeStatus(groupId, email);
            return RedirectToAction(nameof(ExportFileHistory));
        }

        public IActionResult ExportFileHistory(ExportHistoryModel model , EmailSenderModel emailSenderModel)
        {
            TempData["DateTo"] = model.DateTo;
            TempData["DateFrom"] = model.DateFrom;
            return View();
        }
        public JsonResult GetExporttHistoryData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new ExportHistoryModel();
            model.DateTo = Convert.ToDateTime(TempData["DateTo"]);
            model.DateFrom = Convert.ToDateTime(TempData["DateFrom"]);
            var data = model.GetHistories(dataTableAjaxRequestModel);
            return Json(data);
        }
    }
}
