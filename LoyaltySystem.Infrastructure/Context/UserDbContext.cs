using LoyaltySystem.Infrastructure.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Context;

public class UserDbContext:DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
    {
        
    }
    public DbSet<UserEntity> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}