using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data
{
    public class StoreDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public StoreDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        public DbSet<Product> products{ get; set; }
        public DbSet<Stock> stocks { get; set; }
    }
}
