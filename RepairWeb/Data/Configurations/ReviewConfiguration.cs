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

            builder.HasOne<Request>(review => review.Request)
                .WithMany(r => r.Reviews)
                .HasForeignKey(review => review.RequestId);
        }
    }
}
