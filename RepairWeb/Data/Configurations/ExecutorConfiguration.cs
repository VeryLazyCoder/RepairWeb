using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Configurations
{
    public class ExecutorConfiguration : IEntityTypeConfiguration<Executor>
    {
        public void Configure(EntityTypeBuilder<Executor> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Requests)
                .WithOne(r => r.Executor)
                .HasForeignKey(r => r.ExecutorId);

            builder.HasMany(e => e.Reviews)
                .WithOne(rev => rev.Executor)
                .HasForeignKey(rev => rev.ExecutorId);
        }
    }
}
