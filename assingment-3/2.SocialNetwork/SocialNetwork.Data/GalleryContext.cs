using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Data
{
    public class GalleryContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public GalleryContext(string connectionString, string migrationAssemblyName)
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

        public DbSet<Member> members { get; set; }
        public DbSet<Photo> photos { get; set; }
    }
}
