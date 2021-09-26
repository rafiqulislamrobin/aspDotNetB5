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
        (IList<FilePath>records, int total, int totalDisplay) GetImporthistory(int pageIndex, int pageSize,
                                    string searchText, string sortText, Guid id , DateTime DateFrom, DateTime DateTo);
        void CreateGroup(Group group , Guid id);
        void CreateContact(Contact contact);
        (IList<Group> records, int total, int totalDisplay) GetGroupsList(int pageIndex, int pageSize,
                                                  string searchText, Guid id,  string sortText  );
        List<Contact> GetContactList();
        void DeleteGroup(int id);
        Group LoadGroup(int id);
        
      
        string SaveExcelDatatoDb();
        (IList<ExportStatus> records, int total, int totalDisplay) GetExportHistory(int pageIndex, int pageSize,
                                      string searchText, string sortText, Guid id, DateTime DateTo, DateTime DateFrom);
        ExportStatus GetExportHistory(int groupId);
        void UpdateGroup(Group group , Guid id);
        List <Group> LoadAllGroups(Guid id);
        (List<string>, List<List<string>>) ContactList(int groupId);
        void SaveExportHistory(ExportStatus exportStatus);
        void ExportStatus(ExportStatus exportStatus);
    }
}
