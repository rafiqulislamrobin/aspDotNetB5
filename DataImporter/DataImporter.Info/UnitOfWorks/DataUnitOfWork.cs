using DataImporter.Data;
using DataImporter.Info.Context;
using DataImporter.Info.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Info.UnitOfWorks
{
    public class DataUnitOfWork : UnitOfWork, IDataUnitOfWork
    {


        public IFilePathRepository FilePath{ get; private set; }



        public DataUnitOfWork(IDataImporterDbContext context,
             IFilePathRepository filePathRepository)
              : base((DbContext)context)
        {
            FilePath = filePathRepository;
           
        }
    }
}
