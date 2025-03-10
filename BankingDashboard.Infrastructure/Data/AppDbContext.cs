using BankingDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingDashboard.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API configurations (if needed)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Accounts)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId);

        // Add explicit configuration for Transaction entity
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .HasConversion<string>();  // Store enum as string
        
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId);
        
        // Add configuration for TargetAccountId (optional relationship)
        modelBuilder.Entity<Transaction>()
            .HasOne<Account>()
            .WithMany()
            .HasForeignKey(t => t.TargetAccountId)
            .IsRequired(false);  // Make it optional
    }
}
