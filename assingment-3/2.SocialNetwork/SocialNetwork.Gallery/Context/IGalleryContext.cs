using Microsoft.EntityFrameworkCore;
using SocialNetwork.Gallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Gallery.Context
{
     public interface IGalleryContext 
    {
        public DbSet<Member> members { get; set; }
        public DbSet<Photo> photos { get; set; }
    }
}
