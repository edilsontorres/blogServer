using blog_BackEnd.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog_BackEnd.EntityConfiguration
{
    public class CommentsConfiguration : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

            builder.Property(c => c.Author)
            .HasColumnType("VARCHAR(80)")
            .IsRequired();
        }
    }
}