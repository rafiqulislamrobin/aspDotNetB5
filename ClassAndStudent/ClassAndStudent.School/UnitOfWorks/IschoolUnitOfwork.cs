using ClassAndStudent.data;
using ClassAndStudent.School.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.UnitOfWorks
{
    public interface IschoolUnitOfwork :  IUnitOfWork
    {

        IBatchRepository Batch { get;  }

        IStudentRepository Student { get; }
    }
}
