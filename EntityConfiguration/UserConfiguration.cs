using blog_BackEnd.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogServer.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.NameUser)
            .HasColumnType("VARCHAR(80)")
            .IsRequired();

            builder.Property(u => u.Password)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        }
        
    }
}