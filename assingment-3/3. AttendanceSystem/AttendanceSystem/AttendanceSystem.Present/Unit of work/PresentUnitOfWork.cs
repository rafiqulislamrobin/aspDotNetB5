using AttendanceSystem.Data;
using AttendanceSystem.Present.Context;
using AttendanceSystem.Present.Entity;
using AttendanceSystem.Present.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Unit_of_work
{
    public class PresentUnitOfWork : UnitOfWork, IPresentUnitOfWork
    {
        public IAttendanceRepository Attendances { get; set; }
        public IStudentRepository Students { get; set; }

        public PresentUnitOfWork(IAttendanceDbContext context,
            IStudentRepository students,
            IAttendanceRepository attendances)
            : base((DbContext)context)
        {
            Students = students;
            Attendances = attendances;
        }
    }
}
