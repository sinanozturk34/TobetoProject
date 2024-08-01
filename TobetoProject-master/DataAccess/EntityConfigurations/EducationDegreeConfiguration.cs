﻿using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfigurations
{
    public class EducationDegreeConfiguration : IEntityTypeConfiguration<EducationDegree>
    {
        public void Configure(EntityTypeBuilder<EducationDegree> builder)
        {
            builder.ToTable("EducationDegrees").HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name");

            builder.HasMany(b => b.Educations)
               .WithOne(usm => usm.EducationDegree)
               .HasForeignKey(usm => usm.EducationDegreeId);

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
        }
    }
}
