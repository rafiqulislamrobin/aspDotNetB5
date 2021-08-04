using SocialNetwork.Gallery.Busieness_Object;
using SocialNetwork.Gallery.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialNetwork.Gallery.Services
{
    public interface IGalleryServices
    {
       void SavingPhoto(MemberBusinessObject member, PhotoBusinessObject photo);
        IList<MemberBusinessObject> GetAllMembers();
        void CreateMember(MemberBusinessObject member);
        (IList<MemberBusinessObject> records, int total, int totalDisplay) GetMembers(int pageIndex, int pageSize,
                                                              string searchText, string sortText);

        MemberBusinessObject GetMember(int id);
        void UpdateMember(MemberBusinessObject member);
        void DeleteMember(int id);
    }
}
