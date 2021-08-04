using AttendanceSystem.Data;
using AttendanceSystem.Present.Entity;
using AttendanceSystem.Present.Repository;

namespace AttendanceSystem.Present.Unit_of_work
{
    public interface IPresentUnitOfWork : IUnitOfWork
    {
        IAttendanceRepository Attendances { get; set; }
        IStudentRepository Students { get; set; }
    }
}