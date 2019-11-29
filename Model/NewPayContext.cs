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
        public virtual DbSet<Employeeeft> Employeeeft { get; set; }
        public virtual DbSet<Employeeeftaddress> Employeeeftaddress { get; set; }
        public virtual DbSet<Employeenoneft> Employeenoneft { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlite(NewPayDataTransformer.Engine.Config.Settings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "newpaydb");

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

                entity.Property(e => e.Ssn)
                    .IsRequired()
                    .HasColumnName("SSN")
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

            modelBuilder.Entity<Employeeeft>(entity =>
            {
                entity.ToTable("employeeeft", "newpaydb");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("np_EmployeeEft_Employee");

                entity.Property(e => e.Id).HasColumnType("int(10)");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.EmployeeId).HasColumnType("int(10)");

                entity.Property(e => e.RecipientName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.RoutingNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Employeeeft)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("np_EmployeeEft_Employee");
            });

            modelBuilder.Entity<Employeeeftaddress>(entity =>
            {
                entity.ToTable("employeeeftaddress", "newpaydb");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("np_EmployeeEftAddress_Employee");

                entity.Property(e => e.Id).HasColumnType("int(10)");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.BankName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.BankStreetAddress)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.BankStreetAddress2)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.EmployeeId).HasColumnType("int(10)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Employeeeftaddress)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("np_EmployeeEftAddress_Employee");
            });

            modelBuilder.Entity<Employeenoneft>(entity =>
            {
                entity.ToTable("employeenoneft", "newpaydb");

                entity.HasIndex(e => new { e.EmployeeId, e.RecipientName, e.PayPeriodEndDate })
                    .HasName("employeenoneft_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10)");

                entity.Property(e => e.City)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.EmployeeId).HasColumnType("int(10)");

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.RecipientName)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

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

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.ZipCode2)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Employeenoneft)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("np_EmployeeNonEft_Employee");
            });
        }
    }
}
