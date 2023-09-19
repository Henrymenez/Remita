using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remita.Models.Entities.Domians.User;

namespace Remita.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany<ApplicationRole>()
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
