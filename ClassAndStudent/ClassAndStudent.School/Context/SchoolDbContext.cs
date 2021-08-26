using ClassAndStudent.School.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.Context
{
  
    public class SchoolDbContext : DbContext, ISchoolDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public SchoolDbContext(string connectionString, string migrationAssemblyName)
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

            modelBuilder.Entity<Batch>()
                .HasMany(b => b.Students)
                .WithOne(t => t.batch);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Batch> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
