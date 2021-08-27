using Gallery.MemberAndImages.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages.Services
{
    public interface IGalleryService
    {
        void CreateMember(Member member);
        void DeleteMember(int id);
        (IList<Member> records, int total, int totalDisplay) GetMember(int pageIndex, int pageSize,
                                                                string searchText, string sortText);
        void UpdateMember(Member member);
        Member GetMember(int id);
    }
}
