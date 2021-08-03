using AttendanceSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Entity
{
   

    public class Student : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentRollNumber { get; set; }

        public List <Attendance> Attendances { get; set; }

    }
}
