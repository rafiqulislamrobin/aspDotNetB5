using BookAndAuthor.Areas.Admin.Models;
using BookAndAuthor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAndAuthor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger;
        public LibraryController(ILogger<LibraryController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookList()
        {
            var model = new BookListModel();

            return View(model);
        }
        public JsonResult GetBookData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new BookListModel();
            var data = model.GetBooks(dataTableAjaxRequestModel);
            return Json(data);
        }
        public IActionResult Create()
        {
            var model = new CreateBookModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateBookModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateBook();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add Book");
                    _logger.LogError(ex, "Add Book Failed");
                }

            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var model = new EditBookModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Edit(EditBookModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return RedirectToAction(nameof(BookList));
        }
        public IActionResult Delete(int id)
        {
            var model = new CreateBookModel();
            model.Delete(id);
            return RedirectToAction(nameof(BookList));

        }
        public IActionResult AuthorList()
        {
            var model = new AuthorListModel();

            return View(model);
        }
        public JsonResult GetAuthorData()
        {
            var dataTableAjaxRequestModel = new DataTablesAjaxRequestModel(Request);
            var model = new AuthorListModel();
            var data = model.GetAuthor(dataTableAjaxRequestModel);
            return Json(data);
        }
        public IActionResult DeleteAuthor(int id)
        {
            var model = new CreateBookModel();
            model.Delete(id);
            return RedirectToAction(nameof(BookList));

        }
        public IActionResult CreateAuthor()
        {
            var model = new CreateAuthorModel();
            return View(model);
        }


        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult CreateAuthor(CreateAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateAuthors();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Add Author");
                    _logger.LogError(ex, "Add Author Failed");
                }

            }
            return View(model);
        }
        public IActionResult EditAuthor(int id)
        {
            var model = new EditAuthorModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult EditAuthor(EditAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }
            return RedirectToAction(nameof(AuthorList));
        }
    }
}
