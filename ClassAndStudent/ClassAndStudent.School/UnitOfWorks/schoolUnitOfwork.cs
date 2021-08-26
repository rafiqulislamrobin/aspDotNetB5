using ClassAndStudent.data;
using ClassAndStudent.School.Context;
using ClassAndStudent.School.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.UnitOfWorks
{
    public class schoolUnitOfwork : UnitOfWork, IschoolUnitOfwork
    {


        public IBatchRepository Batch { get; private set; }

        public IStudentRepository Student { get; private set; }

        public schoolUnitOfwork(ISchoolDbContext context,
             IBatchRepository batch,
             IStudentRepository student)
              : base((DbContext)context)
        {
            Batch = batch;
            Student = student;
        }
    }
}
