using System;
using System.Collections.Generic;
using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Modelo.HistorialRefreshModel;
using Microsoft.EntityFrameworkCore;


namespace Deportes.Infra;

public class DeportesContext : DbContext
{
    public DbSet<Usuario> Usuario { get; set; }

    public DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }
    public DbSet<Deporte> Deporte { get; set; }
    public DeportesContext(DbContextOptions<DeportesContext> options)
   : base(options)
    {
     
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-TS9IBN4;Initial Catalog=Deportes;Integrated Security=True; TrustServerCertificate=True;");

    // => optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("DefaultConnection"));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Deporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deporte__3214EC07F9A8B3C4");

            entity.ToTable("Deporte");

            entity.Property(e => e.CantJugadores).HasColumnName("cantJugadores");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__03DC48A5643B62D8");

            entity.ToTable("HistorialRefreshToken");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Historial__IdUsu__49C3F6B7");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07678B50A3");

            entity.ToTable("Usuario");

            entity.Property(e => e.Contrasenia)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha_nacimiento");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

    }
}