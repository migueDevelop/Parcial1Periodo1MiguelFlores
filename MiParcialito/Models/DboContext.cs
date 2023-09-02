using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiParcialito.Models;

public partial class DboContext : DbContext
{
    public DboContext()
    {
    }

    public DboContext(DbContextOptions<DboContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=OG-G3;Database=DBo;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.DocenteId).HasName("PK__Docentes__9CB7A9410E1B3F6A");

            entity.Property(e => e.DocenteId).HasColumnName("DocenteID");
            entity.Property(e => e.DocenteName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PK__Inscripc__168316991541D22A");

            entity.Property(e => e.InscripcionId).HasColumnName("InscripcionID");
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__Inscripci__Estud__412EB0B6");

            entity.HasOne(d => d.Materia).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.MateriaId)
                .HasConstraintName("FK__Inscripci__Mater__4222D4EF");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.MateriaId).HasName("PK__Materias__0D019D818F6DF733");

            entity.Property(e => e.MateriaId).HasColumnName("MateriaID");
            entity.Property(e => e.DocenteId).HasColumnName("DocenteID");
            entity.Property(e => e.MateriaName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Docente).WithMany(p => p.Materia)
                .HasForeignKey(d => d.DocenteId)
                .HasConstraintName("FK__Materias__Docent__3E52440B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A3F04C27B");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usuarios__1788CCAC452788C8");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserBirthDate).HasColumnType("date");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Usuarios__RoleID__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
