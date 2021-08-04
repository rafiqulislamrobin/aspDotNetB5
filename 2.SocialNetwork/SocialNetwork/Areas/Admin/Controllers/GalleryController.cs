using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Areas.Admin.Models;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Areas.Admin.Controllers
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
            var model = new GalleryModel();
            model.LoadModelData();
            return View(model);
        }
        public JsonResult GetMemberData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new GalleryModel();
            var data = model.GetMembers(dataTableAjaxRequestModel);
            return Json(data);
        }
        public IActionResult SavingPhoto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SavingPhoto(PhotoSaveModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction(nameof(Index));
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

        public IActionResult Edit(int id)
        {
            var model = new EditMemberModel();
            model.LoadModelData(id);
            return View(model);

        }


        [HttpPost, AutoValidateAntiforgeryToken]

        public IActionResult Edit(EditMemberModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return View(model);
        }
    }
}
