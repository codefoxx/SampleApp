using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SampleApp.Core.Domain;

namespace SampleApp.Persistence.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(course => course.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(course => course.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}