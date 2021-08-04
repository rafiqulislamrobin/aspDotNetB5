using SocialNetwork.Data;
using SocialNetwork.Gallery.Entities;
using SocialNetwork.Gallery.Repository;

namespace SocialNetwork.Gallery.Unit_of_work
{
    public interface IGalleryUnitOfWork :IUnitOfWork
    {
        IMemberRepository Members { get; }
        IPhotoRepository Photos { get; }
    }
}