using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystem.Present.Business_Object;
using AttendanceSystem.Present.Services;
using Autofac;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class CreateStudentModel
    {

        public string Name { get; set; }
        public int StudentRollNumber { get; set; }

        private readonly IAttendanceService _iAttendanceService;
        public CreateStudentModel()
        {
            _iAttendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }
        public CreateStudentModel(IAttendanceService bookingService)
        {
            _iAttendanceService = bookingService;
        }

        internal void CreateStudent()
        {
            var student = new Student()
            {
                Name = Name,
                StudentRollNumber = StudentRollNumber

            };
            _iAttendanceService.CreateStudent(student);
        }
    }
}
