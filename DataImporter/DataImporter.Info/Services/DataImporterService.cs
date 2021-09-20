using AutoMapper;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Exceptions;
using DataImporter.Info.UnitOfWorks;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Info.Services
{
    public class DataImporterService : IDataImporterService
    {
        private readonly IDataUnitOfWork _dataUnitOfWork;
        //private readonly IMapper _mapper;

        public DataImporterService(IDataUnitOfWork dataUnitOfWork/*, IMapper mapper*/)
        {
            _dataUnitOfWork = dataUnitOfWork;
            //_mapper = mapper;

        }


        public (List<string>, List<List<string>>) ContactList(int groupId)
        {
            var group = _dataUnitOfWork.Group.GetById(groupId);
           
            List<string> headers = new();
            List<List<string>> items = new();
            List<string> i = new();
            var contactEntities = _dataUnitOfWork.Contact.GetAll();
            var h = 0;
            foreach (var contactRow in contactEntities)
            {
                if (contactRow.GroupId == group.Id)
                {
                    if (headers.Contains(contactRow.Key))
                    {
                        break;
                    }
                    else
                    {
                        headers.Add(contactRow.Key);
                    }
                }

            }
            foreach (var contactRow in contactEntities)
            {
                if (contactRow.GroupId == group.Id)
                {
                    i.Add(contactRow.Value);
                    h++;
                    if (h == headers.Count)
                    {
                        items.Add(i);
                        i = new List<string>();
                        h = 0;

                    }
                }
            }

            return (headers, items);
        }

        public void CreateContact(Contact contact)
        {
            if (contact == null)
                throw new InvalidParameterException("Customer was not found");



            _dataUnitOfWork.Contact.Add(
                    new Entities.Contact
                    {
                        //Id = contact.Id,
                        //Name = contact.Name,
                        //Address = contact.Address,
                        //GroupId=contact.GroupId

                    }
                   );
            _dataUnitOfWork.Save();
        }

        public void CreateGroup(Group group)
        {
            if (group == null)
                throw new InvalidParameterException("Customer was not found");

            if (IsNameAlreadyUsed(group.Name))
            {
                throw new DuplicateException("Group name is already used");

            }

            _dataUnitOfWork.Group.Add(
                    new Entities.Group
                    {
                        Id = group.Id,
                        Name = group.Name


                    }
                   );
            _dataUnitOfWork.Save();


        }

        public void DeleteGroup(int id)
        {
            _dataUnitOfWork.Group.Remove(id);
            _dataUnitOfWork.Save();
        }

        public List<Contact> GetContactList()
        {
            var contactEntities = _dataUnitOfWork.Contact.GetAll();
            var contacts = new List<Contact>();
            foreach (var item in contactEntities)
            {
                var contact = new Contact()
                {
                    //Id = item.Id,
                    //Name = item.Name,
                    //Address=item.Address,
                    //GroupId =item.GroupId

                };
                contacts.Add(contact);
            }
            return contacts;
        }

        public (IList<Group> records, int total, int totalDisplay) GetGroupsList(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var groupData = _dataUnitOfWork.Group.GetDynamic(
               string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
               sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from groups in groupData.data
                              select new Group
                              {
                                  Id = groups.Id,
                                  Name = groups.Name

                              }).ToList();

            return (resultData, groupData.total, groupData.totalDisplay);
        }

        public (IList<FilePath> records, int total, int totalDisplay) Gethistory(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var historyData = _dataUnitOfWork.FilePath.GetDynamic(
               string.IsNullOrWhiteSpace(searchText) ? null : x => x.FileName.Contains(searchText),
               sortText, string.Empty, pageIndex, pageSize);

            //var resultHistory = (from history in historyData.data
            //                  select _mapper.Map<FilePath>(history)).ToList();
            var resultHistory = (from history in historyData.data
                                 select new FilePath
                                 {
                                     GroupId = history.GroupId,
                                     GroupName = history.GroupName,
                                     FileStatus = history.FileStatus,
                                     FileName = history.FileName,
                                     FilePathName = history.FilePathName,
                                     DateTime =history.DateTime
                                 }).ToList();



            return (resultHistory, historyData.total, historyData.totalDisplay);
        }

        public List<Group> LoadAllGroups()
        {

            var groupEntities = _dataUnitOfWork.Group.GetAll();
            var groups = new List<Group>();
            foreach (var item in groupEntities)
            {
                var group = new Group()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                groups.Add(group);
            }
            return groups;
        }

        public Group LoadGroup(int id)
        {
            var student = _dataUnitOfWork.Group.GetById(id);
            if (student == null)
            {
                return null;
            }
            return new Group
            {
                Id = student.Id,
                Name = student.Name,
            };
        }

        //import worker Service save database method
        public string SaveExcelDatatoDb()
        {

            var fileEntities = _dataUnitOfWork.FilePath.GetAll();
            string fileroot = "";
            string fileStatus = "";
            int fileId = 0;
            int GroupId = 0;
            foreach (var f in fileEntities)
            {
                if (f.FileStatus.ToLower() == "pending")
                {
                    fileroot = f.FilePathName;
                    fileId = f.Id;
                    GroupId = f.GroupId;
                    fileStatus = f.FileStatus = "processing";
                    _dataUnitOfWork.Save();
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var stream = System.IO.File.Open(fileroot, FileMode.Open, FileAccess.Read))
                    {


                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            Contact contact = new();
                            Dictionary<int, Dictionary<string, string>> cont = new();
                            List<string> headers = new();

                            var conf = new ExcelDataSetConfiguration
                            {
                                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                                {
                                    UseHeaderRow = false
                                }
                            };
                            var dataSet = reader.AsDataSet(conf);

                            var dataTable = dataSet.Tables[0];

                            for (var i = 0; i < 1; i++)
                            {
                                for (var j = 0; j < dataTable.Columns.Count; j++)
                                {
                                    headers.Add(dataTable.Rows[i][j].ToString());

                                }

                            }
                            for (var i = 1; i < dataTable.Rows.Count; i++)
                            {
                                Contact contacts = new();
                                for (var j = 0; j < dataTable.Columns.Count; j++)
                                {
                                    var z = dataTable.Rows[i][j].ToString();
                                    if (z != null && z != "")
                                    {
                                        contacts.Properties.Add(headers[j], dataTable.Rows[i][j].ToString());
                                    }
                                    else
                                    {
                                        contacts.Properties.Add(headers[j], "(no value)");
                                    }
                                }
                                cont.Add(i + 1, contacts.Properties);

                            }
                            foreach (var item in cont)
                            {
                                var value = item.Value;

                                foreach (var v in value)
                                {
                                    _dataUnitOfWork.Contact.Add(new Entities.Contact
                                    {
                                        ExcelRow = item.Key,
                                        Key = v.Key,
                                        Value = v.Value,
                                        GroupId = GroupId
                                    });

                                    _dataUnitOfWork.Save();

                                }
                            }

                        }
                    }


                    var file = _dataUnitOfWork.FilePath.GetById(fileId);
                    file.FileStatus = "Completed";
                    _dataUnitOfWork.Save();
                    try
                    {
                        File.Delete(fileroot);
                    }
                    catch (Exception ex)
                    {

                        return ex.Message;
                    }
                    return "Deleted ";

                }
            }
            return "no file to delete";
        }

        public void SaveFilePath(FilePath filepath)
        {


            _dataUnitOfWork.FilePath.Add(
        new Entities.FilePath
        {
            FileName = filepath.FileName,
            FilePathName = filepath.FilePathName,
            DateTime = filepath.DateTime,
            GroupId = filepath.GroupId,
            GroupName = filepath.GroupName,
            FileStatus = filepath.FileStatus



        });
            _dataUnitOfWork.Save();
        }

        public void UpdateGropu(Group group)
        {
            if (group == null)
            {
                throw new InvalidOperationException("Group is missing");
            }
            if (IsNameAlreadyUsed(group.Name))
            {
                throw new DuplicateException("Group name is already used");
            }
            var groupEntity = _dataUnitOfWork.Group.GetById(group.Id);
            if (groupEntity != null)
            {
                groupEntity.Id = group.Id;
                groupEntity.Name = group.Name;


                _dataUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("product is not available");

        }


        private bool IsNameAlreadyUsed(string name) =>
          _dataUnitOfWork.Group.GetCount(n => n.Name == name) > 0;



    }
}
