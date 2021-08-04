using InventorySystem.Store.Entity;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Store.Context
{
    public interface IStoreDbContext
    {
        DbSet<Product> products { get; set; }
        DbSet<Stock> stocks { get; set; }
    }
}