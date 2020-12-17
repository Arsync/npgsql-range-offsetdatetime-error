using InstantIssueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstantIssueApp.Mappings
{
    public class SampleModelMap : IEntityTypeConfiguration<SampleModel>
    {
        public void Configure(EntityTypeBuilder<SampleModel> b)
        {
            b.HasKey(x => x.Id);
            b.ToTable("SampleModels");

            b.Property(x => x.VersionPeriod)
                .HasDefaultValueSql("tstzrange(clock_timestamp(), NULL, '[)')")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            b.Property(x => x.CreationTime)
                .HasDefaultValueSql("clock_timestamp()")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();
        }
    }
}
