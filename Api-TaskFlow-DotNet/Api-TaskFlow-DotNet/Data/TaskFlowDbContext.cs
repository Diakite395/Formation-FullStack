using Api_TaskFlow_DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_TaskFlow_DotNet.Data;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; } = null!;
    public DbSet<Token> Token { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.Entity<User>()
            .HasData([
                new User
                {
                    Id = new Guid("3f9bdfca-75b2-488a-bb36-9290b13bc93d"),
                    Username = "admin",
                    Email = "admin@example.com",
                    Role = "Admin",
                    PasswordHash = "adminpasswordhash",
                    CreatedAt = new DateTime(2025, 11, 5, 15, 41, 32, 132, DateTimeKind.Utc).AddTicks(776)
                }
            ]);
    }
}
