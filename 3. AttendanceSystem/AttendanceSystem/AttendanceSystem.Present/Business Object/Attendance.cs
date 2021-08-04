using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Business_Object
{
    public class Attendance
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int StudentId { get; set; }
    }
}
