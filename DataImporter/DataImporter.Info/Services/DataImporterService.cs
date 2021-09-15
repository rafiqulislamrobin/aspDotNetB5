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

        public (IList<FilePath> records, int total, int totalDisplay) Gethistory(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var historyData = _dataUnitOfWork.FilePath.GetDynamic(
               string.IsNullOrWhiteSpace(searchText) ? null : x => x.FileName.Contains(searchText),
               sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from history in historyData.data
                              select new FilePath
                              {
                                  DateTime = history.DateTime,
                                  FileName = history.FileName,
                                  FilePathName = history.FilePathName,
                                 
                              }).ToList();

            return (resultData, historyData.total, historyData.totalDisplay);
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
