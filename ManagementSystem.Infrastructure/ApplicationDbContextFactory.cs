using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ManagementSystem.Infrastructure;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Updated to match your actual local PostgreSQL credentials
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PipelineManagementDb;Username=postgres;Password=euden123");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}