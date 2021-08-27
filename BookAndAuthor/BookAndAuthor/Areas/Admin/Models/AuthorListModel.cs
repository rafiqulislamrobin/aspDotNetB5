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
    public class AuthorListModel
    {
        private readonly IAuthorService _iAuthorService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthorListModel()
        {
            _iAuthorService = Startup.AutofacContainer.Resolve<IAuthorService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public AuthorListModel(IAuthorService iAuthorService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _iAuthorService = iAuthorService;
        }

        internal object GetAuthor(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iAuthorService.GetAuthors(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Name", "Barcode" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.DateOfBirth.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }

        internal void Delete(int id)
        {
            _iAuthorService.DeleteAuthor(id);
        }
    }
}
