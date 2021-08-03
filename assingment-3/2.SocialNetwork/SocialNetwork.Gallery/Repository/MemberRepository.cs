using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Gallery.Context;
using SocialNetwork.Gallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Gallery.Repository
{
    public class MemberRepository : Repository<Member ,int> , IMemberRepository
    {
        public MemberRepository(IGalleryContext context)
            :base((DbContext)context)
        {

        }

    }
}
