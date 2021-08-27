using Gallery.Areas.Admin.Models;
using Gallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        private readonly ILogger<GalleryController> _logger;
        public GalleryController(ILogger<GalleryController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var model = new CreateMemberModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateMemberModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateMember();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add Member");
                    _logger.LogError(ex, "Add Member Failed");
                }

            }
            return View(model);
        }
        public IActionResult MemberList()
        {
            var model = new MemberListModel();

            return View(model);
        }
        public JsonResult GetMemberData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new MemberListModel();
            var data = model.GetMember(dataTableAjaxRequestModel);
            return Json(data);
        }
        public IActionResult Delete(int id)
        {
            var model = new MemberListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            var model = new MemberEditModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Edit(MemberEditModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return View(model);
        }
    }

}
