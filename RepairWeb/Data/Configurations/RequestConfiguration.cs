using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ClientId).IsRequired();
            builder.Property(r => r.Equipment).IsRequired();
            builder.Property(r => r.ProblemDescription).IsRequired();
            builder.Property(r => r.SerialNumber).IsRequired();
            builder.Property(r => r.ExecutorId).IsRequired(false);
            builder.Property(r => r.ExecutorComment).IsRequired(false);

            builder.HasOne(r => r.Executor)
                .WithMany(e => e.Requests)
                .HasForeignKey(r => r.ExecutorId);

            builder.HasOne(r => r.Report)
                .WithOne(rep => rep.Request);

            builder.HasOne(r => r.Review)
                .WithOne(rev => rev.Request)
                .HasForeignKey<Request>(r => r.ReviewId);
        }
    }
}
