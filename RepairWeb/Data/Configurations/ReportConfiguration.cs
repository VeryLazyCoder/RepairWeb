using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(r => r.Request)
                .WithOne(req => req.Report)
                .HasForeignKey<Report>(r => r.RequestId);

            builder.Property(r => r.Comments).HasMaxLength(120);
            
        }
    }
}
