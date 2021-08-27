using Gallery.Data;
using Gallery.MemberAndImages.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages.Unit_Of_Work
{
     public interface IGalleryUnitOfWork : IUnitOfWork
    {
        IPhotoRepository Photo { get; }
        IMemberRepository Member { get; }
    }
}
