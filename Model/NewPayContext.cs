using System;
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
                optionsBuilder.UseLazyLoadingProxies().UseMySQL(NewPayDataTransformer.Engine.Config.Settings.ConnectionString);
//                optionsBuilder.UseLazyLoadingProxies().UseSqlite(NewPayDataTransformer.Engine.Config.Settings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "newpaydb");

                entity.HasIndex(e => new { e.Agency, e.Emplid })
                    .HasName("employee_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10)");

                entity.Property(e => e.Agency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

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
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Emplid)
                    .IsRequired()
                    .HasColumnName("Emplid")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.StreetAddress2)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.StreetAddress3)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.ZipCode2)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");
            });

        }
    }
}
