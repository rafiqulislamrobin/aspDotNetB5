using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Gallery.Context;
using SocialNetwork.Gallery.Entities;
using SocialNetwork.Gallery.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Gallery.Unit_of_work
{
    public class GalleryUnitOfWork : UnitOfWork, IGalleryUnitOfWork
    {
        public IMemberRepository Members { get; private set; }
        public IPhotoRepository Photos { get; private set; }



        public GalleryUnitOfWork(IGalleryContext context,
            IPhotoRepository  photos,
            IMemberRepository members)

            : base((DbContext)context)
        {
           
        }
    }
}
