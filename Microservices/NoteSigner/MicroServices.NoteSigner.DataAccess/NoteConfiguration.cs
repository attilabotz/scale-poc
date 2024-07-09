using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServices.NoteSigner.DataAccess
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Note");
            builder.HasKey(x => x.Id);
            builder.Property(t => t.Text).IsRequired().HasColumnType("varchar").HasMaxLength(500);
            builder.Property(t => t.CreatedAt).IsRequired().HasColumnType("datetime2");
            builder.Property(t => t.Signature).IsRequired(false).HasColumnType("varchar").HasMaxLength(500);
        }
    }
}
