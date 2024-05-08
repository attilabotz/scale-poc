using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace WebAPI.DataAccess
{
    public class NoteContext : DbContext
    {
        public NoteContext() : base("name=NoteContext")
        {
        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NoteConfiguration());
        }
    }

    public class Note
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class NoteConfiguration : EntityTypeConfiguration<Note>
    {
        public NoteConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.PublicId).IsRequired().HasMaxLength(26);
            this.Property(x => x.Title).IsRequired().HasMaxLength(500);
            this.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
