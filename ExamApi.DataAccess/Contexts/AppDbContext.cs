using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }
    public DbSet<Address> Addresses { get; set; }
}