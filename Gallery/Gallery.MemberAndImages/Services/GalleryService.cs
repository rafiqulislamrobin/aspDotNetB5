using AutoMapper;
using Gallery.MemberAndImages.Business_Object;
using Gallery.MemberAndImages.Exceptions;
using Gallery.MemberAndImages.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryUnitOfWork _iGalleryUnitOfWork;
        private readonly IMapper _mapper;

        public GalleryService(IGalleryUnitOfWork iGalleryUnitOfWork, IMapper mapper)
        {
            _iGalleryUnitOfWork = iGalleryUnitOfWork;
            _mapper = mapper;

        }

        public void CreateMember(Member member)
        {
            if (member == null)
                throw new InvalidParameterException("member was not found");

            if (IsNameAlreadyUsed(member.Name))
                throw new DuplicateException("member Name");

            _iGalleryUnitOfWork.Member.Add(
         _mapper.Map<Entities.Member>(member)
        );
            _iGalleryUnitOfWork.Save();
        }

        public void DeleteMember(int id)
        {
            _iGalleryUnitOfWork.Member.Remove(id);
            _iGalleryUnitOfWork.Save();

        }

        public (IList<Member> records, int total, int totalDisplay) GetMember(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var doctorData = _iGalleryUnitOfWork.Member.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from member in doctorData.data
                              select _mapper.Map<Member>(member)).ToList();


            return (resultData, doctorData.total, doctorData.totalDisplay);
        }

        public Member GetMember(int id)
        {
            var member = _iGalleryUnitOfWork.Member.GetById(id);
            if (member == null)
            {
                return null;
            }

            return _mapper.Map<Member>(member);
        }

        public void UpdateMember(Member member)
        {
            if (member == null)
            {
                throw new InvalidOperationException("Doctor is missing");
            }
            if (IsNameAlreadyUsed(member.Name, member.Id))
            {
                throw new DuplicateException("Doctor name is already used");
            }
            var memberinfo = _iGalleryUnitOfWork.Member.GetById(member.Id);
            if (memberinfo != null)
            {
                _mapper.Map(member, memberinfo);

                _iGalleryUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Doctor is not available");
            }
        }

        private bool IsNameAlreadyUsed(string name) =>
              _iGalleryUnitOfWork.Member.GetCount(n => n.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name, int id) =>
              _iGalleryUnitOfWork.Member.GetCount(n => n.Name == name && n.Id != id) > 0;
    }
}
