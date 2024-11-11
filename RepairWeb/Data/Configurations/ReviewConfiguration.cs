using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Rating).IsRequired();
            builder.Property(p => p.Comment).IsRequired(false);

            builder.HasOne(review => review.Request)
                .WithOne(r => r.Review)
                .HasForeignKey<Review>(r => r.RequestId);
                

            builder.HasOne(rev => rev.Executor)
                .WithMany(e => e.Reviews)
                .HasForeignKey(rev => rev.ExecutorId);
        }
    }
}
