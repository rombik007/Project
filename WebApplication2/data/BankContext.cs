using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Project.Models;

namespace Project.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        

       
        
   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-V7A1AEP;" +
                            "Initial Catalog=Bank;" +
                            "Integrated Security=True;" +
                            "TrustServerCertificate=True;");
            ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Card>()
            //    .Property(c => c.Balance)
            //    .HasColumnType("decimal(18,2)");

            //один аккаунт, багато карт
            modelBuilder.Entity<Account>()
            .HasMany(a => a.Cards)
            .WithOne(c => c.Account)
            .HasForeignKey(c => c.AccountId);

            //Одна карта,багато транзакцій
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CardId)
                      .IsRequired();

                entity.HasOne(e => e.Cards)
                      .WithMany(a => a.Transactions)
                      .HasForeignKey(e => e.CardId)
                      .HasPrincipalKey(x => x.Id);
            });

            // Один обліковий запис на одного клієнта
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(c => c.CustomerId)
                      .IsRequired();

                entity.HasOne(e => e.Customer)
                      .WithOne(c => c.Account)
                      .HasForeignKey<Account>(e => e.CustomerId)
                      .HasPrincipalKey<Customer>(c => c.Id);
            });
            //один працівник, багато аккаунтів
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(c => c.EmployeeID).IsRequired();

                entity.HasOne(e => e.Employees)
                      .WithMany(c => c.Account)
                      .HasForeignKey(e => e.EmployeeID)
                      .HasPrincipalKey(c => c.Id);
            });


        }

    }
    
}
