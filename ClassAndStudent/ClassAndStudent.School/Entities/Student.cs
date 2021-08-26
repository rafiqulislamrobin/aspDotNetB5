using ClassAndStudent.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.Entities
{
    public class Student : IEntity<int>

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentClass { get; set; }
        public Batch batch { get; set; }
    }
}
