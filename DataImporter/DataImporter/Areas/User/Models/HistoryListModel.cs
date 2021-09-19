using Autofac;
using DataImporter.Info.Services;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{

    public class HistoryListModel
    {


        private readonly IDataImporterService _iDataImporterService;
        public HistoryListModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public HistoryListModel(IDataImporterService iDataImporterService)
        {
            _iDataImporterService = iDataImporterService;
        }

        internal object GetHistories(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iDataImporterService.Gethistory(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "FileName", "DateTime", "GroupName" ,"FileStatus" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.FileName.ToString(),
                                record.DateTime.ToString(),
                                record.GroupName.ToString(),
                                record.FileStatus.ToString()
                        }
                    ).ToArray()
            };

        }

    }
}
