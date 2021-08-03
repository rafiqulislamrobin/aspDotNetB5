using AttendanceSystem.Present.Entity;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Present.Context
{
    public interface IAttendanceDbContext
    {
        DbSet<Attendance> attendances { get; set; }
        DbSet<Student> students { get; set; }
    }
}