using AutoMapper;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Exceptions;
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

        public void CreateContact(Contact contact)
        {
            if (contact == null)
                throw new InvalidParameterException("Customer was not found");



            _dataUnitOfWork.Contact.Add(
                    new Entities.Contact
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        Address =contact.Address,
                    



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

            var resultData = (from history in historyData.data
                              select new FilePath
                              {
                                  DateTime = history.DateTime,
                                  FileName = history.FileName,
                                  FilePathName = history.FilePathName,
                                 
                              }).ToList();

            return (resultData, historyData.total, historyData.totalDisplay);
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

        public void SaveFilePath(FilePath member)
        {

            _dataUnitOfWork.FilePath.Add(
         _mapper.Map<Entities.FilePath>(member)
        );
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
