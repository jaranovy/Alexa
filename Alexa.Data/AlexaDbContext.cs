using Alexa.Data.Model;
using System.Data.Entity;

namespace Alexa.Data
{
    public class AlexaDbContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(c => c.Suppliers).WithMany(i => i.Groups)
                .Map(t => t.MapLeftKey("GroupID")
                    .MapRightKey("SupplierID")
                    .ToTable("GroupsToSuppliers"));
        }
    }
}