using BookAndAuthor.Info.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Context
{
    public class LibraryDbContext : DbContext, ILibraryDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public LibraryDbContext(string connectionString, string migrationAssemblyName)
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
            modelBuilder.Entity<BookAuthor>()
                 .HasKey(dp => new { dp.AuthorId, dp.BookId });

            modelBuilder.Entity<BookAuthor>()
                   .HasOne(b => b.Book)
                   .WithMany(a => a.Authors)
                   .HasForeignKey(dp => dp.AuthorId);

            modelBuilder.Entity<BookAuthor>()
                 .HasOne(p => p.Author)
                 .WithMany(l => l.WrittenBooks)
                 .HasForeignKey(dp => dp.BookId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
