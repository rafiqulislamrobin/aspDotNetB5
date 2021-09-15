using Autofac;
using DataImporter.Info.Services;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ViewGroupModel
    {

        private readonly IDataImporterService _iDataImporterService;
        public ViewGroupModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public ViewGroupModel(IDataImporterService iDataImporterService)
        {
            _iDataImporterService = iDataImporterService;
        }

        internal object GetGroups(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iDataImporterService.GetGroupsList(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
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
