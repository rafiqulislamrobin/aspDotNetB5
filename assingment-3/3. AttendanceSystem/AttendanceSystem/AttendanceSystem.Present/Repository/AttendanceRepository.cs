using AttendanceSystem.Data;
using AttendanceSystem.Presenet;
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
    public class AttendanceRepository : Repository<Attendance , int> , IAttendanceRepository
    {
        public AttendanceRepository(IAttendanceDbContext context)
            :base((DbContext)context)
        {

        }
    }
}
