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
    public class DoctorRepository : Repository<Doctor, int>, IDoctorRepository
    {
        public DoctorRepository(IChemberContext context)
    : base((DbContext)context)
        {

        }
    }
    
    
}
