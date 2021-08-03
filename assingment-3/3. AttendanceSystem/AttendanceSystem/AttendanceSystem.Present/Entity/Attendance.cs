using AttendanceSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Entity
{
    public class Attendance : IEntity<int>
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }


    }
}
