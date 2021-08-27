using Autofac;
using BookAndAuthor.Info.Services;
using BookAndAuthor.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAndAuthor.Areas.Admin.Models
{

    public class BookListModel
    {
        private readonly IBookService _iBookService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookListModel()
        {
            _iBookService = Startup.AutofacContainer.Resolve<IBookService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public BookListModel(IBookService iBookService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _iBookService = iBookService;
        }

        internal object GetBooks(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iBookService.GetBooks(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Title", "Barcode", "Price" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Title,
                                record.Barcode,
                                record.Price.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }

        internal void Delete(int id)
        {
            _iBookService.DeleteBook(id);
        }
    }
}
