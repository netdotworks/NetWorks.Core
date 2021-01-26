using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace NetWorks.Core.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(DatabaseContext).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries().Where(w => w.State == EntityState.Added))
            {
                item.CurrentValues["DateCreated"] = DateTime.Now;
                item.CurrentValues["IsDeleted"] = 0;
            }

            foreach (var item in ChangeTracker.Entries().Where(w => w.State == EntityState.Modified))
            {
                item.CurrentValues["DateUpdated"] = DateTime.Now;
            }

            foreach (var item in ChangeTracker.Entries().Where(w => w.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["DateDeleted"] = DateTime.Now;
                item.CurrentValues["IsDeleted"] = 1;
            }

            return base.SaveChanges();
        }
    }
}