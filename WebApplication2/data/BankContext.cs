using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Project.Models
{
    public class BankContext : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-V7A1AEP;" +
                            "Initial Catalog=Bank;" +
                            "Integrated Security=True;" +
                            "TrustServerCertificate=True;");
            ;
        }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public  DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .Property(c => c.Balance)
                .HasColumnType("decimal(18,2)");
            //modelBuilder.Entity<Transactions>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.AccID)
            //          .IsRequired();

            //    entity.HasOne(e => e.Accounts)
            //          .WithMany(a => a.Transactions)
            //          .HasForeignKey(e => e.AccountId)
            //          .HasPrincipalKey(x => x.id);
            //});
                        ///одна карта, багато транзакцій
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CardID)
                      .IsRequired();

                entity.HasOne(e => e.Cards)
                      .WithMany(a => a.Transactions)
                      .HasForeignKey(e => e.AccountId)
                      .HasPrincipalKey(x => x.id);
            });
            //один аккаунт, багато карт
            modelBuilder.Entity<Account>() 
            .HasMany(a => a.Cards)
            .WithOne(c => c.Account)
            .HasForeignKey(c => c.AccountId);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerId)
                      .IsRequired();

                entity.HasOne(e => e.Customer)
                      .WithOne(c => c.Account)
                      .HasForeignKey<Account>(e => e.CustomerId)
                      .HasPrincipalKey<Customer>(c => c.Id);
            });

        }


    }
}
