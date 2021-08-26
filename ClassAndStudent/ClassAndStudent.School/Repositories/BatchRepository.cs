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
    public class BatchRepository : Repository<Batch, int>, IBatchRepository
    {
        public BatchRepository(ISchoolDbContext context)
    : base((DbContext)context)
        {

        }
    }
}
