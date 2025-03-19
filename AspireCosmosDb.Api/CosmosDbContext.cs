using Microsoft.EntityFrameworkCore;

namespace AspireCosmosDb.Api
{
    public class Entry
    {
        private Guid _id;
        private Guid _schemaId;

        public Guid Id
        {
            get => _id == Guid.Empty ? _id = Guid.NewGuid() : _id;
            set => _id = value;
        }

        public string? Name { get; set; }

        public Guid SchemaId
        {
            get => _schemaId == Guid.Empty ? _schemaId = Guid.NewGuid() : _schemaId;
            set => _schemaId = value;
        }
    }

    public class Schema
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public class CosmosDbContext(DbContextOptions<CosmosDbContext> options) : DbContext(options)
    {
        public DbSet<Entry> Entries { get; set; }

        public DbSet<Schema> Schemas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>().ToContainer("Entry").HasPartitionKey(x => x.SchemaId);
            modelBuilder.Entity<Schema>().ToContainer("Schema").HasPartitionKey(x => x.Id);
        }
    }
}
