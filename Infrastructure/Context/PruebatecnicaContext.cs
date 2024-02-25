using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class PruebatecnicaContext : DbContext
{
    public PruebatecnicaContext()
    {
    }

    public PruebatecnicaContext(DbContextOptions<PruebatecnicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Localidade> Localidades { get; set; }

    public virtual DbSet<Mercado> Mercados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Localidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("localidades");

            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Mercado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mercados");

            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Apellidos).HasMaxLength(255);
            entity.Property(e => e.Ciudad).HasMaxLength(255);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(25);
            entity.Property(e => e.Pais).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vehiculos");

            entity.HasIndex(e => e.LocalidadDevolucionId, "LocalidadDevolucionId");

            entity.HasIndex(e => e.LocalidadRecogidaId, "LocalidadRecogidaId");

            entity.HasIndex(e => e.MercadoId, "MercadoId");

            entity.Property(e => e.Disponible).HasDefaultValueSql("'1'");
            entity.Property(e => e.Marca).HasMaxLength(255);
            entity.Property(e => e.Modelo).HasMaxLength(255);

            entity.HasOne(d => d.LocalidadDevolucion).WithMany(p => p.VehiculoLocalidadDevolucions)
                .HasForeignKey(d => d.LocalidadDevolucionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehiculos_ibfk_2");

            entity.HasOne(d => d.LocalidadRecogida).WithMany(p => p.VehiculoLocalidadRecogida)
                .HasForeignKey(d => d.LocalidadRecogidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehiculos_ibfk_1");

            entity.HasOne(d => d.Mercado).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.MercadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehiculos_ibfk_3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
