using AttendanceSystem.Present.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AttendanceSystem.Present.Business_Object;

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
    }
}
