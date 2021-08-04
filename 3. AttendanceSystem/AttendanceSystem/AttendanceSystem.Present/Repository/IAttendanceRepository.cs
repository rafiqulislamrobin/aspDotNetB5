using AttendanceSystem.Data;
using AttendanceSystem.Present.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Repository
{
    public interface IAttendanceRepository : IRepository<Attendance, int>
    {
    }
}
