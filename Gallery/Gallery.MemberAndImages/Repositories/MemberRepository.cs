using Gallery.Data;
using Gallery.MemberAndImages.Context;
using Gallery.MemberAndImages.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages.Repositories
{
    public class MemberRepository : Repository<Member, int>, IMemberRepository
    {
        public MemberRepository(IGalleryDbContext context)
    : base((DbContext)context)
        {

        }
    }
}
