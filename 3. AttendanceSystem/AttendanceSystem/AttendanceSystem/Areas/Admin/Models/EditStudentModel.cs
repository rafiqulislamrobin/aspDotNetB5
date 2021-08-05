using AttendanceSystem.Present.Business_Object;
using AttendanceSystem.Present.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class EditStudentModel

    {

   
        public int? Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        [Required, Range(0, 100)]
        public int? StudentRollNumber { get; set; }
       

        private readonly IAttendanceService _attendanceService;
        public EditStudentModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }
        public EditStudentModel(IAttendanceService AttendanceService)
        {
            _attendanceService = AttendanceService;
        }

        public void LoadModelData(int id)
        {
            var student = _attendanceService.GetStudent(id);

            Id = student?.Id;
            Name = student?.Name;
            StudentRollNumber = student?.StudentRollNumber;
           
        }

        internal void Update()
        {
            var student = new Student
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                StudentRollNumber = StudentRollNumber.HasValue ? StudentRollNumber.Value : 0

            };
            _attendanceService.UpdateStudent(student);
        }
    }
}
