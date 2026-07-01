using ManagerApplicationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerApplicationSystem.Config
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(b => b.Id).ValueGeneratedNever();

            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);

            builder.Property(b => b.Age).IsRequired();

            builder.Property(b => b.Grade).IsRequired();
        }
    }
}
