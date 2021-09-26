using Autofac;
using DataImporter.Common.Utility;
using DataImporter.Info.Business_Object;
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
    public class ExportHistoryModel
    {
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }

        private readonly IDataImporterService _iDataImporterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExportHistoryModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public ExportHistoryModel(IDataImporterService iDataImporterService, IHttpContextAccessor httpContextAccessor)
        {
            _iDataImporterService = iDataImporterService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal object GetHistories(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {
            var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var data = _iDataImporterService.GetExportHistory(
                    dataTableAjaxRequestModel.PageIndex,
                    dataTableAjaxRequestModel.PageSize,
                    dataTableAjaxRequestModel.SearchText,
                    dataTableAjaxRequestModel.GetSortText(new string[] { "GroupName", "EmailStatus", "Id", "DateTime" }),
                      id, DateTo,DateFrom);
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName.ToString(),
                               record.EmailStatus.ToString(),
                                record.Id.ToString(),
                                record.DateTime.ToString()

                        }
                    ).ToArray()
            };

        }



    }
}
