using System;
using System.Collections.Generic;
using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Modelo.HistorialRefreshModel;
using Microsoft.EntityFrameworkCore;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.FichaDeporteModel;

namespace Deportes.Infra;

public class DeportesContext : DbContext
{
  
    public DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }
   
    public  DbSet<Deporte> Deporte { get; set; }

    public  DbSet<Evento> Evento { get; set; }
    public  DbSet<FichaDeporte> FichaDeporte { get; set; }
    public  DbSet<Participante> Participante { get; set; }

    public  DbSet<Usuario> Usuario { get; set; }

    public DeportesContext(DbContextOptions<DeportesContext> options)
   : base(options)
    {
     
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Deporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deporte__3214EC07DAC436D9");

            entity.ToTable("Deporte");

            entity.Property(e => e.CantJugadores).HasColumnName("cantJugadores");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__Evento__034EFC04DE28FBA0");

            entity.ToTable("Evento");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDeporteNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdDeporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evento__IdDeport__29572725");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evento__IdUsuari__286302EC");
        });

        modelBuilder.Entity<FichaDeporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FichaDep__3214EC071FBECE5C");

            entity.ToTable("FichaDeporte");

            entity.Property(e => e.Altura)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Avatar)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Edad)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ManoHabil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Peso)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PieHabil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Posicion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDeporteNavigation).WithMany(p => p.FichaDeportes)
                .HasForeignKey(d => d.IdDeporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ficha_Deporte");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.FichaDeportes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ficha_Usuario");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__03DC48A5721F7B3A");

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
                .HasConstraintName("FK__Historial__IdUsu__30F848ED");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.IdParticipantes).HasName("PK__Particip__229AD7211CEB6F67");

            entity.ToTable("Participante");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Participantes)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__IdEve__2C3393D0");

            entity.HasOne(d => d.IdUsuarioCreadorEventoNavigation).WithMany(p => p.ParticipanteIdUsuarioCreadorEventoNavigations)
                .HasForeignKey(d => d.IdUsuarioCreadorEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__IdUsu__2E1BDC42");

            entity.HasOne(d => d.IdUsuarioParticipanteNavigation).WithMany(p => p.ParticipanteIdUsuarioParticipanteNavigations)
                .HasForeignKey(d => d.IdUsuarioParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__IdUsu__2D27B809");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07F7EDAD4D");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellido).HasMaxLength(255);
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Localidad).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Numero).HasMaxLength(100);
            entity.Property(e => e.Provincia).HasMaxLength(255);
        });
        
    }
}