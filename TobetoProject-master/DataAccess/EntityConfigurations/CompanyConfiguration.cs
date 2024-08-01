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
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies").HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name");
            builder.Property(b => b.Url).HasColumnName("Url");

            builder.HasMany(b => b.CourseCompanies)
            .WithOne(b => b.Company)
            .HasForeignKey(b => b.CompanyId);

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
        }
    }
}
