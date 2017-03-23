
using CoreSandbox.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSandbox.Domain
{
    /// <summary>
    ///     Default Startup class for CoreSandbox.Domain, only used to support dotnet-ef tool.
    /// </summary>
    public class Startup
    {
        public Startup() { }

        public void ConfigureServices(IServiceCollection services)
        {
            System.Console.WriteLine("Startup CoreSandbox.Domain");
            services.AddDbContext<CoreSandboxContext>(options =>
                options.UseNpgsql("Host=localhost;Database=aspnetcore-sandbox;Username=postgres;Password=postgres")
            );
        }
    }
}