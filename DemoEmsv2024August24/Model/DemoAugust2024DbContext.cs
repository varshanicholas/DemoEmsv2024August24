using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoEmsv2024August24.Model;

public partial class DemoAugust2024DbContext : DbContext
{
    public DemoAugust2024DbContext()
    {
    }

    public DemoAugust2024DbContext(DbContextOptions<DemoAugust2024DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblDepartment> TblDepartments { get; set; }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =SHAJITH-NICHOLA\\SQLEXPRESS; Initial Catalog = DemoAugust2024DB; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__tblDepar__B2079BED0B47D2D5");

            entity.ToTable("tblDepartment");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__tblEmplo__7AD04F115D9C84C0");

            entity.ToTable("tblEmployees");

            entity.Property(e => e.Contact)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.DateOfJoining).HasColumnType("date");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.TblEmployees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblEmploy__Depar__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
