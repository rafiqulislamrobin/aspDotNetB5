using DataImporter.Areas.User.Models;
using DataImporter.Info.Business_Object;
using DataImporter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                    if (ex.Message== "Group name is already used")
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
        public IActionResult ViewContacts()
        {
            return View();
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

        [HttpPost]
        public async Task<IActionResult> ImportFileAsync(FilePathModel filePathModel, IFormFile file)
        {
            
            if (filePathModel.GroupId ==0)
            {
                
                var model = new FilePathModel();
                var list = model.LoadAllGroups();
                ViewBag.GroupList = new SelectList(list, "Id", "Name");
                return View(model);
            }
            else
            {
                var groupId = filePathModel.GroupId;
                FilePathModel model = new();
                await model.SaveFilePathAsync(file, groupId);
                
            }
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
