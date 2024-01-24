using Microsoft.EntityFrameworkCore;

namespace MicroService.Producer.DataAccess
{
    public class ProducerContext : DbContext
    {
      
        public ProducerContext(DbContextOptions<ProducerContext> options)
            : base(options)
        { }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProducerContext).Assembly);
        }
    }
}
