using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartCampus.API.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // Design-time connection string (sadece migration oluşturmak için)
        var connectionString = "Server=localhost;Database=smartcampus_db;User=root;Password=rootpassword;";
        
        // MySQL 8.0 sürümünü manuel belirt (AutoDetect yerine)
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
        optionsBuilder.UseMySql(connectionString, serverVersion);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

