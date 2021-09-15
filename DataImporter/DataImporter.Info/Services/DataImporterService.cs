using AutoMapper;
using DataImporter.Info.Business_Object;
using DataImporter.Info.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Info.Services
{
    public class DataImporterService : IDataImporterService
    {
        private readonly IDataUnitOfWork _dataUnitOfWork;
        private readonly IMapper _mapper;

        public DataImporterService(IDataUnitOfWork dataUnitOfWork, IMapper mapper)
        {
            _dataUnitOfWork = dataUnitOfWork;
            _mapper = mapper;

        }

        public void SaveFilePath(FilePath member)
        {

            _dataUnitOfWork.FilePath.Add(
         _mapper.Map<Entities.FilePath>(member)
        );
            _dataUnitOfWork.Save();
        }
    }
}
