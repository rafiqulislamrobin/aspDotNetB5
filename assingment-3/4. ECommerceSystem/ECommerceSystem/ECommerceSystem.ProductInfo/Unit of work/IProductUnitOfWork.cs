using ECommerceSystem.Data;
using ECommerceSystem.ProductInfo.Entity;
using ECommerceSystem.ProductInfo.Repositories;

namespace ECommerceSystem.ProductInfo.Unit_of_work
{
    public interface IProductUnitOfWork : IUnitOfWork
    {
        IProductRepository Products { get; set; }
    }
}