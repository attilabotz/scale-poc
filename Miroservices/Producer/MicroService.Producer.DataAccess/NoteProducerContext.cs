using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MicroService.Producer.DataAccess
{
    public class NoteProducerContext : DbContext
    {
        private readonly ILogger<DbContext> _logger;

        public NoteProducerContext(DbContextOptions<NoteProducerContext> options, ILogger<DbContext> logger)
            : base(options)
        {
            _logger = logger;
        }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoteProducerContext).Assembly);
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }

        public override void Dispose()
        {
            _logger.LogDebug("Disposing NoteProducerContext.");
            base.Dispose();
        }
    }
}
