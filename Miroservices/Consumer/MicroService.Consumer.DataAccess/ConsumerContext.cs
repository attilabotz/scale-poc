using Microsoft.EntityFrameworkCore;

namespace MicroService.Consumer.DataAccess
{
    public class ConsumerContext : DbContext
    {
      
        public ConsumerContext(DbContextOptions<ConsumerContext> options)
            : base(options)
        { }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsumerContext).Assembly);
        }
    }
}
