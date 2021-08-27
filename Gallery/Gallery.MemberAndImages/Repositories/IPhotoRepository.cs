using Gallery.Data;
using Gallery.MemberAndImages.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages.Repositories
{
    public interface IPhotoRepository : IRepository<Photo, int>
    {
    }
}
