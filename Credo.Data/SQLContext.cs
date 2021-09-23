using Credo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Credo.Data
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> option) : base(option) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<LoanRequst> LoanRequst { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Customer

            modelBuilder.Entity<Customer>()
                .HasKey(k => k.Id);
           
            modelBuilder.Entity<Customer>()
                .HasMany(k => k.LoanRequst);

            modelBuilder.Entity<Customer>()
                .Property(p => p.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(p => p.Lastname).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(p => p.Username).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(p => p.PasswordHash).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(p => p.PasswordSalt).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(p => p.PersonalNumber).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(p => p.Phone).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(p => p.Email).HasMaxLength(100);

            #endregion

            #region LoanRequest

            modelBuilder.Entity<LoanRequst>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<LoanRequst>()
                .Property(p => p.Amount).IsRequired();
            modelBuilder.Entity<LoanRequst>()
                .Property(p => p.Currency).HasMaxLength(5).IsRequired();
            modelBuilder.Entity<LoanRequst>()
                .Property(p => p.From).IsRequired();
            modelBuilder.Entity<LoanRequst>()
                .Property(p => p.To).IsRequired();
            modelBuilder.Entity<LoanRequst>()
                .Property(p => p.ActionDate).IsRequired();
            modelBuilder.Entity<LoanRequst>()
                .Property(p => p.Type).IsRequired();
            modelBuilder.Entity<LoanRequst>()
                .Property(p => p.LoanStatus).IsRequired();

            modelBuilder.Entity<LoanRequst>()
                .HasOne(c => c.Customer)
                .WithMany(l => l.LoanRequst)
                .OnDelete(DeleteBehavior.NoAction); ;

            #endregion

            #region Log

            modelBuilder.Entity<Log>()
               .HasKey(k => k.Id);
            modelBuilder.Entity<Log>()
               .Property(k => k.Body).IsRequired();

            #endregion
        }
    }
}
