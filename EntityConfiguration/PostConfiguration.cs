using blog_BackEnd.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog_BackEnd.EntityConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Slug)
            .IsUnique(true);

            builder.Property(p => p.Title)
            .HasColumnType("VARCHAR(80)")
            .IsRequired();

            builder.Property(p => p.Content)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

            builder.Property(p => p.Author)
            .HasColumnType("VARCHAR(80)")
            .IsRequired();

            builder.Property(p => p.CoverImg)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        }
    }
}