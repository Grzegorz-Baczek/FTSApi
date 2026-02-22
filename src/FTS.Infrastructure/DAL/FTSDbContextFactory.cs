using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FTS.Infrastructure.DAL;

internal sealed class FTSDbContextFactory : IDesignTimeDbContextFactory<FTSDbContext>
{
    public FTSDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FTSDbContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=ftsdb;Trusted_Connection=True;TrustServerCertificate=True");
        return new FTSDbContext(optionsBuilder.Options);
    }
}
