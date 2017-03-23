using CoreSandbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreSandbox.Domain.Data
{
    public class CoreSandboxContext : DbContext
    {
        public CoreSandboxContext(DbContextOptions<CoreSandboxContext> options) : base(options)
        {
            System.Console.WriteLine($"CoreSandboxContext - options = {options}");
        }

        public CoreSandboxContext(DbContextOptions options) : base(options)
        {
            System.Console.WriteLine($"CoreSandboxContext - options = {options}");
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
