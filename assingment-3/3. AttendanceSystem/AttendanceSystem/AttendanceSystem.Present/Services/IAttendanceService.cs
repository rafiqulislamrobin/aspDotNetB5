using AttendanceSystem.Present.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Services
{
    public interface IAttendanceService
    {
        public void CheckingPresent(Attendance attendance, Student student);
        public IList<Student> GetAllStudent();
        void CreateStudent(Student student);
    }
}
