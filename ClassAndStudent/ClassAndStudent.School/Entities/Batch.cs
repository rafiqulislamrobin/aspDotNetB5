using ClassAndStudent.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.Entities
{
    public  class Batch : IEntity<int>

    {
        public int Id { get; set; }
        public int ClassNumber { get; set; }
        public int RoomNumber { get; set; }
        public List<Student> Students { get; set; }

    }
}
