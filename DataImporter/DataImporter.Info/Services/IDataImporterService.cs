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
        (IList<FilePath>records, int total, int totalDisplay) Gethistory(int pageIndex, int pageSize,
                                                          string searchText, string sortText);
        void CreateGroup(Group group);
        void CreateContact(Contact contact);
        (IList<Group> records, int total, int totalDisplay) GetGroupsList(int pageIndex, int pageSize,
                                                  string searchText, string sortText);
        List<Contact> GetContactList();
        void DeleteGroup(int id);
        Group LoadGroup(int id);
        void UpdateGropu(Group group);
        List <Group> LoadAllGroups();
    }
}
