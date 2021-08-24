using DoctorAndPatient.Chember.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Context
{
    public class ChemberContext : DbContext, IChemberContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ChemberContext(string connectionString, string migrationAssemblyName)
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
            modelBuilder.Entity<DoctorPatient>()
                 .HasKey(dp => new { dp.DoctorId, dp.PatientId });

            modelBuilder.Entity<DoctorPatient>()
                   .HasOne(d => d.Doctor)
                   .WithMany(l => l.ListOfPatients)
                   .HasForeignKey(dp => dp.DoctorId);

            modelBuilder.Entity<DoctorPatient>()
                 .HasOne(p => p.Patient)
                 .WithMany(l => l.ListOfDoctors)
                 .HasForeignKey(dp => dp.DoctorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }

}
