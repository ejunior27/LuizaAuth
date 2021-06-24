using Domain.LuizaAuth.Entities;
using Infra.LuizaAuth.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.LuizaAuth.Context
{
    public class LuizaAuthContext : DbContext
    {
        public LuizaAuthContext(DbContextOptions<LuizaAuthContext> options)
            :base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RecoveryPasswordMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
