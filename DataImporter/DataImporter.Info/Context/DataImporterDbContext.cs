
using DataImporter.Info.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Info.Context
{

    public class DataImporterDbContext : DbContext, IDataImporterDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public DataImporterDbContext(string connectionString, string migrationAssemblyName)
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
            modelBuilder.Entity<Group>()
                .HasMany(b => b.Contacts)
                .WithOne(t => t.Group);


            modelBuilder.Entity<Group>()
              .HasMany(b => b.FilePaths)
              .WithOne(t => t.Group);
        }

        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
