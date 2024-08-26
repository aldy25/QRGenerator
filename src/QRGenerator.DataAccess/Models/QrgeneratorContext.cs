using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QRGenerator.DataAccess.Models;

public partial class QrgeneratorContext : DbContext
{
    public QrgeneratorContext()
    {
    }

    public QrgeneratorContext(DbContextOptions<QrgeneratorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Qrcodegenerate> Qrcodegenerates { get; set; }

    public virtual DbSet<Qrreader> Qrreaders { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-3UPBPKFI;Initial Catalog=QRGenerator;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Qrcodegenerate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__qrcodege__3213E83FC050D2C4");

            entity.ToTable("qrcodegenerate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Qrcodegenerates)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__qrcodegene__text__267ABA7A");
        });

        modelBuilder.Entity<Qrreader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__qrreader__3213E83FFDBB6A88");

            entity.ToTable("qrreader");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Qrreaders)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__qrreader__userna__29572725");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__user__F3DBC57364116466");

            entity.ToTable("user");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
