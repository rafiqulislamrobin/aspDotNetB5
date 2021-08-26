using ClassAndStudent.data;
using ClassAndStudent.School.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.Repositories
{
    public interface IStudentRepository  : IRepository<Student, int>
    {
    }
}
