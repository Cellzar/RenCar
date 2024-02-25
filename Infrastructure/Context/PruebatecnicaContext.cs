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

    public virtual DbSet<Alquilere> Alquileres { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Localidade> Localidades { get; set; }

    public virtual DbSet<Mercado> Mercados { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alquilere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alquileres");

            entity.HasIndex(e => e.ClienteId, "ClienteID");

            entity.HasIndex(e => e.VehiculoId, "VehiculoID");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("alquileres_ibfk_1");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.VehiculoId)
                .HasConstraintName("alquileres_ibfk_2");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Localidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("localidades");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Mercado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mercados");

            entity.HasIndex(e => e.LocalidadId, "LocalidadID");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.LocalidadId).HasColumnName("LocalidadID");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Localidad).WithMany(p => p.Mercados)
                .HasForeignKey(d => d.LocalidadId)
                .HasConstraintName("mercados_ibfk_1");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vehiculos");

            entity.HasIndex(e => e.LocalidadId, "LocalidadID");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.LocalidadId).HasColumnName("LocalidadID");
            entity.Property(e => e.Marca).HasMaxLength(100);
            entity.Property(e => e.Modelo).HasMaxLength(100);
            entity.Property(e => e.Disponible).HasColumnType("TINYINT(1)");

            entity.HasOne(d => d.Localidad).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.LocalidadId)
                .HasConstraintName("vehiculos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
