using DoctorAndPatient.Chember.Context;
using DoctorAndPatient.Chember.Entities;
using DoctorAndPatient.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Repositories
{
    public class PatientRepository : Repository<Patient, int>, IPatientRepository
    {
        public PatientRepository(IChemberContext context)
    : base((DbContext)context)
        {

        }
    }
}
