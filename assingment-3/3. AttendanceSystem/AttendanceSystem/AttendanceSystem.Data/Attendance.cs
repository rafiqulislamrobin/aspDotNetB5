using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Data
{
    public class Attendance
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public Student studentId { get; set; }
    }
}
