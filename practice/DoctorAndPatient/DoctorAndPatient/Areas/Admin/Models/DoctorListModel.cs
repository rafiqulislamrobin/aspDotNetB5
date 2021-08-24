using Autofac;
using DoctorAndPatient.Chember.Services;
using DoctorAndPatient.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAndPatient.Areas.Admin.Models
{
    public class DoctorListModel
    {
        private readonly IChemberService _iChemberService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DoctorListModel()
        {
            _iChemberService = Startup.AutofacContainer.Resolve<IChemberService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public DoctorListModel(IChemberService iChemberService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _iChemberService = iChemberService;
        }

        internal object GetDoctors(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iChemberService.GetDoctors(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Name", "Department", "Degree" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Department,
                                record.Degree,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }
     
    }
}
