using CoreSandbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreSandbox.Domain.Data
{
    public class CoreSandboxContext : DbContext
    {
        public CoreSandboxContext() : base() { }

        public CoreSandboxContext(DbContextOptions<CoreSandboxContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
