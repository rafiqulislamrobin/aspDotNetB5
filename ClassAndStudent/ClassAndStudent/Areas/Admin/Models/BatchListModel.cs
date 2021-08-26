using Autofac;
using ClassAndStudent.Models;
using ClassAndStudent.School.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAndStudent.Areas.Admin.Models
{

    public class BatchListModel
    {
        private readonly ISchoolService _SchoolService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BatchListModel()
        {
            _SchoolService = Startup.AutofacContainer.Resolve<ISchoolService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public BatchListModel(ISchoolService schoolService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _SchoolService = schoolService;
        }

        internal object GetBatch(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _SchoolService.GetBatches(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "ClassNumber", "RoomNumber" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                
                                record.ClassNumber.ToString(),
                                record.RoomNumber.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }

        internal void Delete(int id)
        {
            _SchoolService.DeleteBatch(id);
        }
    }
}
