using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroService.Consumer.DataAccess
{
    public  class NoteConfiguration  : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Note");
            builder.HasKey(x => x.Id);
            builder.Property(t => t.Text).HasColumnType("varchar").HasMaxLength(500);
            builder.Property(t => t.CreatedAt).HasColumnType("datetime2");
        }
    }
}
