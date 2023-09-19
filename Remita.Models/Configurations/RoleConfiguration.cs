using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remita.Models.Entities.Domians.User;

namespace Remita.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasMany<ApplicationRole>()
               .WithOne()
               .HasForeignKey(ur => ur.Id)
               .IsRequired();
        }
    }
}
