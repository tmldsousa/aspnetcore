using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CoreSandbox.Domain.Data;

namespace CoreSandbox
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // fix for heroku postgres add-on
            var envDatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            Console.WriteLine($"Environment 'DATABASE_URL'={envDatabaseUrl}");
            if (!string.IsNullOrEmpty(envDatabaseUrl))
            {
                Console.WriteLine("This application seems to be hosted on heroku with PostgresSql add-on. Reading DATABASE_URL environment var.");

                Uri dbUri = new Uri(envDatabaseUrl);
                string host = dbUri.Host;
                int port = dbUri.Port;
                string database = dbUri.AbsolutePath.Substring(1);
                string username = dbUri.UserInfo.Split(':')[0];
                string password = dbUri.UserInfo.Split(':')[1];

                string dbConnString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
                Console.WriteLine($"New connection string: {dbConnString}");
                Environment.SetEnvironmentVariable("ConnectionStrings:DataAccessPostgreSqlProvider", dbConnString);
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Entity framework
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");

            services.AddDbContext<CoreSandboxContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("CoreSandbox.Domain")
                )
            );
            
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
