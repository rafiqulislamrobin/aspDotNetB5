using Autofac;
using DataImporter.Common.Utility;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ExportHistoryModel
    {

            private readonly IDataImporterService _iDataImporterService;
            public ExportHistoryModel()
            {
                _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
            }
            public ExportHistoryModel(IDataImporterService iDataImporterService)
            {
                _iDataImporterService = iDataImporterService;
            }

            internal object GetHistories(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
            {

                var data = _iDataImporterService.GetExportHistory(
                    dataTableAjaxRequestModel.PageIndex,
                    dataTableAjaxRequestModel.PageSize,
                    dataTableAjaxRequestModel.SearchText,
                    dataTableAjaxRequestModel.GetSortText(new string[] { "GroupName", "EmailStatus", "DownloadStatus", "DateTime" }));
                return new
                {
                    recordsTotal = data.total,
                    recordsFiltered = data.totalDisplay,
                    data = (from record in data.records
                            select new string[]
                            {
                                record.GroupName.ToString(),
                               record.EmailStatus.ToString(),
                                record.DownloadStatus,
                                record.DateTime.ToString()
                              
                            }
                        ).ToArray()
                };

            }

        

    }
}
