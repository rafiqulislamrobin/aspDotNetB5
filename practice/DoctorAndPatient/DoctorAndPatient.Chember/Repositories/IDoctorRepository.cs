
using DoctorAndPatient.Chember.Entities;
using DoctorAndPatient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Repositories
{
    public interface IDoctorRepository :   IRepository<Doctor, int>
    {
    }
}
