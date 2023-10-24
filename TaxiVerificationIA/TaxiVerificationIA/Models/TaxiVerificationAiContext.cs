using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaxiVerificationIA.Models;

public partial class TaxiVerificationAiContext : DbContext
{
    public TaxiVerificationAiContext()
    {
    }

    public TaxiVerificationAiContext(DbContextOptions<TaxiVerificationAiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TaxiDriver> TaxiDrivers { get; set; }

    public virtual DbSet<Taxis> Taxes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    public virtual DbSet<VerificationsImage> VerificationsImages { get; set; }

    public virtual DbSet<VerificationsResult> VerificationsResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); DataBase=TaxiVerificationAI; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.IdAgent).HasName("PK__Agent__92FC7C03086E757A");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Agents)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Agents__IdUser__5812160E");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.IdBrand).HasName("PK__Brands__662A665907C02733");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.IdColor).HasName("PK__Colors__E83D55CBC4A57191");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.IdModel).HasName("PK__Models__C2F00099E6377345");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584CACB909D3");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TaxiDriver>(entity =>
        {
            entity.HasKey(e => e.IdTaxiDriver).HasName("PK__TaxiDriv__638DA7DDC38E071B");

            entity.Property(e => e.DriverLicense)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTaxiNavigation).WithMany(p => p.TaxiDrivers)
                .HasForeignKey(d => d.IdTaxi)
                .HasConstraintName("FK__TaxiDrive__IdTax__59063A47");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TaxiDrivers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__TaxiDrive__IdUse__59FA5E80");
        });

        modelBuilder.Entity<Taxis>(entity =>
        {
            entity.HasKey(e => e.IdTaxi).HasName("PK__Taxis__9FCAC9ADC2E99B44");

            entity.ToTable("Taxis");

            entity.Property(e => e.Plate)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBrandNavigation).WithMany(p => p.Taxes)
                .HasForeignKey(d => d.IdBrand)
                .HasConstraintName("FK__Taxis__IdBrand__5AEE82B9");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.Taxes)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("FK__Taxis__IdColor__5CD6CB2B");

            entity.HasOne(d => d.IdModelNavigation).WithMany(p => p.Taxes)
                .HasForeignKey(d => d.IdModel)
                .HasConstraintName("FK__Taxis__IdModel__5BE2A6F2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__B7C92638E71BAECF");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Users__IdRol__571DF1D5");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.HasKey(e => e.IdVerification).HasName("PK__Verifica__577A89387ECEC84A");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Folio)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAgentNavigation).WithMany(p => p.Verifications)
                .HasForeignKey(d => d.IdAgent)
                .HasConstraintName("FK__Verificat__IdAge__6477ECF3");

            entity.HasOne(d => d.IdTaxiNavigation).WithMany(p => p.Verifications)
                .HasForeignKey(d => d.IdTaxi)
                .HasConstraintName("FK__Verificat__IdTax__628FA481");

            entity.HasOne(d => d.IdTaxiDriverNavigation).WithMany(p => p.Verifications)
                .HasForeignKey(d => d.IdTaxiDriver)
                .HasConstraintName("FK__Verificat__IdTax__6383C8BA");
        });

        modelBuilder.Entity<VerificationsImage>(entity =>
        {
            entity.HasKey(e => e.IdVerificationImages).HasName("PK__Verifica__ABADCF37FB7AFE6C");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FrontImageName).HasMaxLength(50);
            entity.Property(e => e.FrontalImage).HasMaxLength(1);
            entity.Property(e => e.LeftImageName).HasMaxLength(50);
            entity.Property(e => e.LeftSideImage).HasMaxLength(1);
            entity.Property(e => e.RightImageName).HasMaxLength(50);
            entity.Property(e => e.RightSideImage).HasMaxLength(1);

            entity.HasOne(d => d.IdVerificationNavigation).WithMany(p => p.VerificationsImages)
                .HasForeignKey(d => d.IdVerification)
                .HasConstraintName("FK__Verificat__IdVer__656C112C");
        });

        modelBuilder.Entity<VerificationsResult>(entity =>
        {
            entity.HasKey(e => e.IdVerificationResult).HasName("PK__Verifica__25C6D9192EC23785");

            entity.Property(e => e.ColorMatchAvg).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LabelsMatchAvg).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PlateMatchAvg).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.VerificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdVerificationNavigation).WithMany(p => p.VerificationsResults)
                .HasForeignKey(d => d.IdVerification)
                .HasConstraintName("FK__Verificat__IdVer__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
