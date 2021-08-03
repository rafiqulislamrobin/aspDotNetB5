using Microsoft.EntityFrameworkCore;
using SocialNetwork.Gallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Gallery.Context
{ 
    public class GalleryContext : DbContext ,IGalleryContext
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasMany(b => b.Photos)
                .WithOne(p => p.Member);
                

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Member> members { get; set; }
        public DbSet<Photo> photos { get; set; }
    }
}
