using Microsoft.EntityFrameworkCore;
using SocialNetwork.Gallery.Busieness_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Gallery.Context;
using SocialNetwork.Gallery.Unit_of_work;
using SocialNetworl.Common.Utility;
using TicketBookingSystem.Booking.Exceptions;

namespace SocialNetwork.Gallery.Services
{
    public class GalleryServices : IGalleryServices
    {
        private readonly IGalleryUnitOfWork _galleryUnitOfWork;
        private readonly IDatetimeUtility _datetimeUtility;
        public GalleryServices(IGalleryUnitOfWork galleryUnitOfWork, IDatetimeUtility datetimeUtility)
        {
            _galleryUnitOfWork = galleryUnitOfWork;
            _datetimeUtility = datetimeUtility;
        }
        public IList<MemberBusinessObject> GetAllMembers()
        {
            var memberEntities = _galleryUnitOfWork.Members.GetAll();

            var members = new List<MemberBusinessObject>();

            foreach (var entity in memberEntities)
            {
                var member = new MemberBusinessObject()
                {
                    Name = entity.Name,
                    Address = entity.Address,
                    DateOfBirth =entity.DateOfBirth

                };
                members.Add(member);
            }
            return members;
        }

        public void CreateMember(MemberBusinessObject member)
        {
 
            if (IsNameAlreadyUsed(member.Name))
                throw new DuplicateException("Member Name already exist");

            _galleryUnitOfWork.Members.Add(
                new Entities.Member
                {

                    Name = member.Name,
                    DateOfBirth = member.DateOfBirth,
                    Address = member.Address
                });

            _galleryUnitOfWork.Save();
        }
        public void SavingPhoto(MemberBusinessObject member , PhotoBusinessObject photo)
        {
            var memberEntity = _galleryUnitOfWork.Members.GetById(member.Id);

            if (memberEntity ==null)
            {
                throw new InvalidOperationException("Member was not found!");
            }
            if (memberEntity.Photos==null)
                memberEntity.Photos = new List<Entities.Photo>();


            memberEntity.Photos.Add(new Entities.Photo
            {
                PhotoFileName =photo.PhotoFileName
            });

            _galleryUnitOfWork.Save();
        }

        public (IList<MemberBusinessObject> records, int total, int totalDisplay) GetMembers(int pageIndex, int pageSize,
      string searchText, string sortText)
        {
            var memberData = _galleryUnitOfWork.Members.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name == searchText,
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from member in memberData.data
                              select new MemberBusinessObject
                              {
                                  Id = member.Id,
                                  Name = member.Name,
                                  DateOfBirth = member.DateOfBirth,
                                  Address = member.Address
                                
                              }).ToList();

            return (resultData, memberData.total, memberData.totalDisplay);
        }
            private bool IsNameAlreadyUsed(string name) =>
                  _galleryUnitOfWork.Members.GetCount(n => n.Name == name) > 0;
            private bool IsNameAlreadyUsed(string name, int id) =>
                 _galleryUnitOfWork.Members.GetCount(n => n.Name == name && n.Id != id) > 0;

        public MemberBusinessObject GetMember(int id)
        {

            var member = _galleryUnitOfWork.Members.GetById(id);
            if (member == null)
            {
                return null;
            }

            return new MemberBusinessObject
            {
                Id = member.Id,
                Name = member.Name,
                DateOfBirth = member.DateOfBirth,
                Address = member.Address


            };
        }

        public void UpdateMember(MemberBusinessObject member)
        {
            if (member == null)
            {
                throw new InvalidOperationException("member is missing");
            }
            if (IsNameAlreadyUsed(member.Name, member.Id))
            {
                throw new DuplicateException("member name is already used");
            }
            var memberEntity = _galleryUnitOfWork.Members.GetById(member.Id);
            if (memberEntity != null)
            {
                memberEntity.Id = member.Id;
                memberEntity.Name = member.Name;
                memberEntity.DateOfBirth = member.DateOfBirth;
                memberEntity.Address = member.Address;
                _galleryUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("member is not available");
            }

        }
    }
}
