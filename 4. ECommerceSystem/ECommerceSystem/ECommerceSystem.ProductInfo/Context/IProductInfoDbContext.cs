using ECommerceSystem.ProductInfo.Entity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSystem.ProductInfo
{
    public interface IProductInfoDbContext
    {
        DbSet<Product> Products { get; set; }
    }
}