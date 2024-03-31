
using KLTN20T102433.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KLTN20T102433.Infrastructure.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //builder.Property(r => r.UserName)
            //   .HasColumnType("varchar(100)")
            //   .HasMaxLength(100)
            //   .IsRequired();
        }
    }
}
