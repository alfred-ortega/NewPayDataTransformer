﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NewPayDataTransformer.Model
{
    public partial class NewPayContext : DbContext
    {
        public NewPayContext()
        {
        }

        public NewPayContext(DbContextOptions<NewPayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseMySQL("Server=172.21.17.100;Database=newpay;Uid=aortega;Pwd=Passw0rd$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "newpay");

                entity.HasIndex(e => new { e.Agency, e.Ssn })
                    .HasName("employee_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10)");

                entity.Property(e => e.Agency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.PayPeriodEndDate).HasColumnType("date");

                entity.Property(e => e.Ssn)
                    .IsRequired()
                    .HasColumnName("SSN")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress2)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress3)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Suffix)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode2)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });
        }
    }
}