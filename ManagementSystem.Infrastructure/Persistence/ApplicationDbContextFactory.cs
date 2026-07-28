using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ManagementSystem.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(
            "Server=coinbelsh.postgres.database.azure.com;Database=projectmanagment;Port=5432;User Id=Laconics;Password=McKresha2024!;Ssl Mode=Require;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
