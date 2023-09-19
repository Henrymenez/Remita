using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Remita.Data.Configurations;
using Remita.Models.Entities.Domians.Course;
using Remita.Models.Entities.Domians.Delivery;
using Remita.Models.Entities.Domians.Invoice;
using Remita.Models.Entities.Domians.Result;
using Remita.Models.Entities.Domians.Transcript;
using Remita.Models.Entities.Domians.User;

namespace Remita.Models.DatabaseContexts;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (IMutableForeignKey relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(builder);
        builder.Entity<DeliveryFee>()
        .HasIndex(x => x.StateId)
              .IsUnique();


        builder.Entity<IdentityUserRole<string>>()
       .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Entity<TranscriptApplication>()
       .HasOne(ai => ai.ApplicationInvoice)
       .WithOne(ta => ta.Application)
       .HasForeignKey<InvoiceApplication>(ta => ta.Id).IsRequired(false);


        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<DeliveryFee> DeliveryFees { get; set; }
    public DbSet<InvoiceApplication> ApplicationInvoices { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<ReviewResult> ReviewResults { get; set; }
    public DbSet<StudentTranscript> StudentTranscripts { get; set; }
    public DbSet<TranscriptApplication> TranscriptApplications { get; set; }
    public DbSet<TranscriptApplicationFee> TranscriptApplicationFees { get; set; }
    public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
}
