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
                optionsBuilder.UseLazyLoadingProxies().UseSqlite(NewPayDataTransformer.Engine.Config.Settings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id)
                    .HasColumnName("ID");
//                    .ValueGeneratedNever();

                entity.Property(e => e.Agency)
                    .IsRequired()
                    .HasColumnType("VARCHAR(3)");

                entity.Property(e => e.City)
                    .HasColumnType("VARCHAR(75)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasColumnType("VARCHAR(10)");

                entity.Property(e => e.Emplid)
                    .IsRequired()
                    .HasColumnType("VARCHAR(15)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(35)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(35)");

                entity.Property(e => e.MiddleName)
                    .HasColumnType("VARCHAR(35)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasColumnType("VARCHAR(10)");

                entity.Property(e => e.State)
                    .HasColumnType("VARCHAR(2)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StreetAddress)
                    .HasColumnType("VARCHAR(75)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StreetAddress2)
                    .HasColumnType("VARCHAR(75)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ZipCode)
                    .HasColumnType("VARCHAR(10)")
                    .HasDefaultValueSql("'NULL'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
