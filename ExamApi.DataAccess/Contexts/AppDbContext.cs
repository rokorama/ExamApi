using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
                .HasOne(u => u.PersonalInfo)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<PersonalInfo>()
                .HasOne(pi => pi.Address)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
}

    public DbSet<User> Users { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }
    public DbSet<Address> Addresses { get; set; }
}