using AttendanceSystem.Present.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AttendanceSystem.Present.Business_Object;
using AttendanceSystem.Models;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class AttendanceModel
    {
        public IList<Student> Students { get; set; }
        private readonly IAttendanceService _attendanceService;
        public AttendanceModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }
        public AttendanceModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public void LoadModelData()
        {
            Students = _attendanceService.GetAllStudent();
        }
        internal object GetStudents(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _attendanceService.GetStudents(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] {  "StudentRollNumber", "Name" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.StudentRollNumber.ToString(),
                                record.Name,  
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }

        internal void Delete(int id)
        {
            _attendanceService.DeleteStudent(id);
        }
    }
}
