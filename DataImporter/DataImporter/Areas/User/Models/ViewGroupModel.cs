using Autofac;
using DataImporter.Info.Services;
using DataImporter.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ViewGroupModel
    {

        private readonly IDataImporterService _iDataImporterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGroupServices _groupServices;
        public ViewGroupModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            _groupServices = Startup.AutofacContainer.Resolve<IGroupServices>();
        }
        public ViewGroupModel(IDataImporterService iDataImporterService , 
            IHttpContextAccessor httpContextAccessor, IGroupServices groupServices)
        {
            _iDataImporterService = iDataImporterService;
            _httpContextAccessor = httpContextAccessor;
            _groupServices = groupServices;
        }

        internal object GetGroups(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {
            var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var data = _groupServices.GetGroupsList(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                 id,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Name" }));


            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name.ToString(),
                                record.Id.ToString()

                                
                        }
                    ).ToArray()
            };

        }

    }
}
