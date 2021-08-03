using AttendanceSystem.Presenet;
using AttendanceSystem.Present.Business_Object;
using AttendanceSystem.Present.Context;
using AttendanceSystem.Present.Unit_of_work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Exceptions;
using Student = AttendanceSystem.Present.Business_Object.Student;

namespace AttendanceSystem.Present.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IPresentUnitOfWork _PresentUnitOfWork;
        public AttendanceService(IPresentUnitOfWork PresentUnitOfWork)
        {
            _PresentUnitOfWork = PresentUnitOfWork;
        }





        public   IList<Student> GetAllStudent()
        {
            var studentEntities = _PresentUnitOfWork.Students.GetAll();
            var students = new List<Student>();

            foreach (var entity in studentEntities)
            {
                var student = new Student()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    StudentRollNumber = entity.StudentRollNumber
                };
                students.Add(student);
            }
            return students;
        }
        public void CheckingPresent(Attendance attendance, Student student)
        {
            var studentEntity = _PresentUnitOfWork.Students.GetById(student.Id);

            if (studentEntity == null)
            {
                throw new InvalidOperationException("Course was not found");
            }
            if (studentEntity.Attendances == null)
                studentEntity.Attendances = new List<Entity.Attendance>();


            studentEntity.Attendances.Add(new Entity.Attendance
            {
                Date = attendance.Date,
                StudentId = attendance.StudentId

            });

            _PresentUnitOfWork.save();
        }

        public void CreateStudent(Student student)
        {
            if (student == null)
                throw new InvalidParameterException("Student was not found");

            if (IsNameAlreadyUsed(student.Name))
                throw new DuplicateException("Student Name ");

            _PresentUnitOfWork.Students.Add(
                    new Entity.Student
                    {
                        Name = student.Name,
                        StudentRollNumber =student.StudentRollNumber,  
                    }
                   );
            _PresentUnitOfWork.save();

        }
        private bool IsNameAlreadyUsed(string name) => 
              _PresentUnitOfWork.Students.GetCount(n => n.Name == name) > 0;


    }
}
