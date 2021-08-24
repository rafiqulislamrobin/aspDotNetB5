using DoctorAndPatient.Chember.Repositories;
using DoctorAndPatient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Unit_Of_Work
{
    public interface IChemberUnitOfWork :  IUnitOfWork
    {
        IPatientRepository Patient { get; }
        IDoctorRepository Doctor { get; }
     }

}
