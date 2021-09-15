﻿using DataImporter.Data;
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

        public IGroupRepository Group { get; private set; }
        public IContactRepository Contact { get; private set; }

        //IFilePathRepository IDataUnitOfWork.Group => throw new NotImplementedException();

        public DataUnitOfWork(IDataImporterDbContext context,
             IFilePathRepository filePathRepository,
             IGroupRepository group,
             IContactRepository contact)
              : base((DbContext)context)
        {
            FilePath = filePathRepository;
            Group = group;
            Contact = contact;


        }
    }
}
