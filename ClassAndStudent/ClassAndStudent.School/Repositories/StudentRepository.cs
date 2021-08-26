using ClassAndStudent.data;
using ClassAndStudent.School.Context;
using ClassAndStudent.School.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.Repositories
{
    public class StudentRepository : Repository<Student, int>, IStudentRepository
    {
        public StudentRepository(ISchoolDbContext context)
    : base((DbContext)context)
        {

        }
    }
    
}
