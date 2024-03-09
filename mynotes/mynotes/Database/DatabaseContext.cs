using Microsoft.EntityFrameworkCore;
using mynotes.Models;
using System.Security.Principal;

namespace mynotes.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAccountEntity(modelBuilder);
            ConfigureNotesEntity(modelBuilder);
        }

        private void ConfigureAccountEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Users)
                .WithMany(a => a.Account)
                .HasForeignKey(a => a.UserId); 
        }

        private void ConfigureNotesEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notes>()
                .HasOne(n => n.Account)
                .WithMany(a => a.Notes)
                .HasForeignKey(n => n.AccountId);
        }

    }
}
