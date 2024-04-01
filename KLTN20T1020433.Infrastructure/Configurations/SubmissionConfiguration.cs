
using KLTN20T1020433.DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KLTN20T1020433.Infrastructure.Configurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            //builder.Property(r => r.RoleName)
            //    .HasColumnType("varchar(100)")
            //    .HasMaxLength(100)
            //    .IsRequired();

            
        }
    }
}
