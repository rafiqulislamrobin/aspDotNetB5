
using DoctorAndPatient.Chember.Context;
using DoctorAndPatient.Chember.Repositories;
using DoctorAndPatient.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Unit_Of_Work
{
    public class ChemberUnitOfWork : UnitOfWork , IChemberUnitOfWork
    {
       

        public IPatientRepository Patient { get; private set; }

        public IDoctorRepository Doctor { get; private set; }

        public ChemberUnitOfWork(IChemberContext context,
             IPatientRepository patient,
             IDoctorRepository doctor)
              : base((DbContext)context)
        {
            Patient = patient;
            Doctor = doctor;
        }
    }
}

