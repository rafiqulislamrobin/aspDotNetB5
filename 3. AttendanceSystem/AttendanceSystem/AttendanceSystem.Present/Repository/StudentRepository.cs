using AttendanceSystem.Data;
using AttendanceSystem.Present.Context;
using AttendanceSystem.Present.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Repository
{
    public class StudentRepository : Repository<Student,  int> , IStudentRepository
    {
        public StudentRepository(IAttendanceDbContext context)
            :base ((DbContext)context)
        {

        }
    }
}
