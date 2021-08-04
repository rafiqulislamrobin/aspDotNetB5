using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystem.Present.Business_Object;
using AttendanceSystem.Present.Services;
using Autofac;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class CreateStudentModel
    {

        public int? Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        [Required, Range(0, 100)]
        public int? StudentRollNumber { get; set; }


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
                StudentRollNumber = StudentRollNumber.Value

            };
            _iAttendanceService.CreateStudent(student);
        }
    }
}
