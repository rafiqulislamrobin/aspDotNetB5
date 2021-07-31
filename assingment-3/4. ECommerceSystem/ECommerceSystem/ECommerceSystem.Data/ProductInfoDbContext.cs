using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Data
{
    public class ProductInfoDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ProductInfoDbContext(string connectionString , string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }
        }
        public DbSet<Product> products { get; set; }
    }
}
