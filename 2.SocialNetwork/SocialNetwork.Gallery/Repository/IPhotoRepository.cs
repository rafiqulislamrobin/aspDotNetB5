using SocialNetwork.Data;
using SocialNetwork.Gallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Gallery.Repository
{
    public interface IPhotoRepository :  IRepository<Photo, int>
    {
    }
}
