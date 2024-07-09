using Microsoft.EntityFrameworkCore;

namespace MicroServices.NoteSigner.DataAccess
{
    public class NoteSignerContext : DbContext
    {

        public NoteSignerContext(DbContextOptions<NoteSignerContext> options)
            : base(options)
        { }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoteSignerContext).Assembly);
        }
    }
}
