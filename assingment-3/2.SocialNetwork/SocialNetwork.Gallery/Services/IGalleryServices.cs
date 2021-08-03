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
    }
}
