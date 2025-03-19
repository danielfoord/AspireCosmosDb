using Microsoft.EntityFrameworkCore;

namespace AspireCosmosDb.Api
{
    public class Entry
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public class CosmosDbContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        public CosmosDbContext(DbContextOptions<CosmosDbContext> options)
           : base(options)
            {
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>().ToContainer("Entry");
        }
    }
}
