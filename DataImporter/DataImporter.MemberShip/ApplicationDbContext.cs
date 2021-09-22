﻿using DataImporter.MemberShip.DataSeeds;
using DataImporter.MemberShip.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace DataImporter.MemberShip
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ApplicationDbContext(string connectionString, string migrationAssemblyName)
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .HasData(DataSeed.Roles);

            base.OnModelCreating(builder);
        }
    }
}
