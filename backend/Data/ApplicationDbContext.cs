using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Models;

namespace SmartCampus.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets will be added here as we create models
    // public DbSet<User> Users { get; set; }
    // public DbSet<Student> Students { get; set; }
    // public DbSet<Faculty> Faculties { get; set; }
    // public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Model configurations will be added here
    }
}

