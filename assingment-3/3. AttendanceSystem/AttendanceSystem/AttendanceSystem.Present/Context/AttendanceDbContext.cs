using AttendanceSystem.Present.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Present.Context
{
    public class AttendanceDbContext : DbContext, IAttendanceDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public AttendanceDbContext(string connectionString, string migrationAssemblyName)
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
            modelBuilder.Entity<Student>()
                .HasMany(b => b.Attendances)
                .WithOne(t => t.Student);
                

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> students { get; set; }
        public DbSet<Attendance> attendances { get; set; }
    }
}
