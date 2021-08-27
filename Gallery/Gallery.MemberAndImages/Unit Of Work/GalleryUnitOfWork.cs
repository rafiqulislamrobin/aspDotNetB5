using Gallery.Data;
using Gallery.MemberAndImages.Context;
using Gallery.MemberAndImages.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages.Unit_Of_Work
{
    public class GalleryUnitOfWork : UnitOfWork, IGalleryUnitOfWork
    {


        public IPhotoRepository Photo { get; private set; }

        public IMemberRepository Member { get; private set; }

        public GalleryUnitOfWork(IGalleryDbContext context,
             IPhotoRepository photo,
             IMemberRepository member)
              : base((DbContext)context)
        {
            Photo = photo;
            Member = member;
        }
    }
}
