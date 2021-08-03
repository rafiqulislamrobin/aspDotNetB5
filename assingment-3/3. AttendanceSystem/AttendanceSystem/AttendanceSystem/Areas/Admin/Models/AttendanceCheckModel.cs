using AttendanceSystem.Present.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AttendanceSystem.Present.Business_Object;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class AttendanceCheckModel
    {
        public int attendanceId { get; set; }
        public string studentName { get; set; }

        private readonly IAttendanceService _attendanceService;
        public AttendanceCheckModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }
        public AttendanceCheckModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        public void CheckingPresent()
        {
            var students = _attendanceService.GetAllStudent();
            var selectecdStudent = students.Where(x => x.Name == studentName).FirstOrDefault();

            var attendance = new Attendance()
            {

                Date = DateTime.Now,
                StudentId = 2


            };
            _attendanceService.CheckingPresent(attendance, selectecdStudent);
        }
    }
}
