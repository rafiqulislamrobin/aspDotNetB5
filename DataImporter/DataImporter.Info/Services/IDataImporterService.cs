using DataImporter.Info.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Info.Services
{
    public interface IDataImporterService
    {
        void SaveFilePath(FilePath member);
        (IList<FilePath> records, int total, int totalDisplay) Gethistory(int pageIndex, int pageSize,
                                                          string searchText, string sortText);
    }
}
