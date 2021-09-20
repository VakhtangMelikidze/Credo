using Credo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Data
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> option) : base(option) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<LoanRequst> LoanRequst { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Customer

            modelBuilder.Entity<Customer>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Customer>()
                .Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Customer>()
                .Property(p => p.Lastname).HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(p => p.Username).HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(p => p.PasswordHash).HasMaxLength(1000);
            modelBuilder.Entity<Customer>()
                .Property(p => p.PasswordSalt).HasMaxLength(1000);
            modelBuilder.Entity<Customer>()
                .Property(p => p.PersonalNumber).HasMaxLength(20);
            modelBuilder.Entity<Customer>()
                .Property(p => p.Phone).HasMaxLength(50);
            modelBuilder.Entity<Customer>()
                .Property(p => p.Email).HasMaxLength(100);

            #endregion

            #region LoanRequest

            modelBuilder.Entity<LoanRequst>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<LoanRequst>()
               .Property(p => p.Currency).HasMaxLength(5);

            modelBuilder.Entity<LoanRequst>()
                .HasOne(c => c.Customer)
                .WithMany(l => l.LoanRequst)
                .OnDelete(DeleteBehavior.NoAction); ;

            #endregion

        }
    }
}
