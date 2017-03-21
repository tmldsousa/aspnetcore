using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoreSandbox.Domain.Data
{
    public class PostgreSqlCoreSandboxContextFactory : IDbContextFactory<CoreSandboxContext>
    {
        public CoreSandboxContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<CoreSandboxContext>();
            builder.UseNpgsql(
                "Host=localhost;Database=aspnetcore-sandbox;Username=postgres;Password=postgres",
                b => b.MigrationsAssembly("CoreSandbox.Domain")
            );

            return new CoreSandboxContext(builder.Options);
        }
    }
}
