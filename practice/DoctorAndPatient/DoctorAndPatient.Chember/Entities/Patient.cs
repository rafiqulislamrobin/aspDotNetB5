using DoctorAndPatient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Entities
{
    public class Patient  : IEntity<int>
    
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string ProblemName { get; set; }

        public List<DoctorPatient> ListOfDoctors { get; set; }
    }
}
