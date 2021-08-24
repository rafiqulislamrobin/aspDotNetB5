using DoctorAndPatient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Entities
{
    public class Doctor : IEntity<int>
    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Degree { get; set; }

        public List<DoctorPatient> ListOfPatients { get; set; }
    }
}
