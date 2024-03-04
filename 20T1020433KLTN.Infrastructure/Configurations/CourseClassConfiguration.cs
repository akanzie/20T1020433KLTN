
using _20T1020433KLTN.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace _20T1020433KLTN.Infrastructure.Configurations
{
    public class CourseClassConfiguration : IEntityTypeConfiguration<CourseClass>
    {
        public void Configure(EntityTypeBuilder<CourseClass> builder)
        {
        }
    }
}
