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

            if (IsRollNumberAlreadyUsed(student.StudentRollNumber))
                throw new DuplicateException("Student Roll Number Already Used");

            _PresentUnitOfWork.Students.Add(
                    new Entity.Student
                    {
                        Name = student.Name,
                        StudentRollNumber =student.StudentRollNumber,  
                    }
                   );
            _PresentUnitOfWork.save();

        }

        public (IList<Student> records, int total, int totalDisplay) GetStudents(int pageIndex, int pageSize,
        string searchText, string sortText)
        {
            var studentData = _PresentUnitOfWork.Students.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name == searchText,
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from student in studentData.data
                              select new Student
                              {
                                  Id = student.Id,
                                  StudentRollNumber = student.StudentRollNumber,
                                  Name = student.Name,
                                  
                               
                              }).ToList();

            return (resultData, studentData.total, studentData.totalDisplay);
        }

        public Student GetStudent(int id)
        {

            var student = _PresentUnitOfWork.Students.GetById(id);
            if (student == null)
            {
                return null;
            }

            return new Student
            {
                Id = student.Id,
                Name = student.Name,
                StudentRollNumber = student.StudentRollNumber,
               


            };
        }

        public void UpdateStudent(Student student)
        {
            if (student == null)
            {
                throw new InvalidOperationException("student is missing");
            }
            if (IsNameAlreadyUsed(student.Name, student.Id))
            {
                throw new DuplicateException("student name is already used");
            }
            var studentEntity = _PresentUnitOfWork.Students.GetById(student.Id);
            if (studentEntity != null)
            {
                studentEntity.Id = student.Id;
                studentEntity.Name = student.Name;
                studentEntity.StudentRollNumber = student.StudentRollNumber;
               
                _PresentUnitOfWork.save();
            }
            else
            {
                throw new InvalidOperationException("student is not available");
            }

        }
        private bool IsRollNumberAlreadyUsed(int rollNumber) =>
           _PresentUnitOfWork.Students.GetCount(n => n.StudentRollNumber == rollNumber) > 0;
        private bool IsNameAlreadyUsed(string name, int id) =>
           _PresentUnitOfWork.Students.GetCount(n => n.Name == name && n.Id != id) > 0;

        public void DeleteStudent(int id)
        {
            _PresentUnitOfWork.Students.Remove(id);
            _PresentUnitOfWork.save();
        }
    }
}
