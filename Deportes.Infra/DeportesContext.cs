using System;
using System.Collections.Generic;
using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Modelo.HistorialRefreshModel;
using Microsoft.EntityFrameworkCore;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.FichaDeporteModel;
using Deportes.Modelo.FichaBasquetModel;
using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
using Deportes.Modelo.FichaTenisModel;
using Deportes.Modelo.CalificacionModel;

namespace Deportes.Infra;

public class DeportesContext : DbContext
{
    public  DbSet<Calificacion> Calificacions { get; set; }
    public DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }
    public  DbSet<Deporte> Deporte { get; set; }
    public  DbSet<Evento> Evento { get; set; }
    public  DbSet<FichaDeportistum> FichaDeportista { get; set; }
    public  DbSet<FichaFutbol> FichaFutbols { get; set; }
    public  DbSet<Participante> Participante { get; set; }
    public  DbSet<Usuario> Usuario { get; set; }
    public virtual DbSet<FichaBasquet> FichaBasquet { get; set; }
    public virtual DbSet<FichaTeni> FichaTeni { get; set; }

    public DeportesContext(DbContextOptions<DeportesContext> options)
   : base(options)
    {
     
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Califica__3214EC0786B70E49");

            entity.ToTable("Calificacion");

            entity.Property(e => e.Calificacion1).HasColumnName("calificacion");

            entity.HasOne(d => d.IdEventoParticipoNavigation).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.IdEventoParticipo)
                .HasConstraintName("FK_Calificacion_evento");

            entity.HasOne(d => d.IdUsuarioCalificadoNavigation).WithMany(p => p.CalificacionIdUsuarioCalificadoNavigations)
                .HasForeignKey(d => d.IdUsuarioCalificado)
                .HasConstraintName("FK_Calificacion_usuarioCalificado");

            entity.HasOne(d => d.IdUsuarioCalificadorNavigation).WithMany(p => p.CalificacionIdUsuarioCalificadorNavigations)
                .HasForeignKey(d => d.IdUsuarioCalificador)
                .HasConstraintName("FK_Calificacion_usuario");
        });

        modelBuilder.Entity<Deporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deporte__3214EC07891614B8");

            entity.ToTable("Deporte");

            entity.Property(e => e.CantJugadores).HasColumnName("cantJugadores");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__Evento__034EFC04E8419F4C");

            entity.ToTable("Evento");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Finalizado).HasComputedColumnSql("(case when [Fecha] IS NULL then NULL else case when CONVERT([date],[Fecha])<CONVERT([date],getdate()) then CONVERT([bit],(1)) else CONVERT([bit],(0)) end end)", false);
            entity.Property(e => e.Localidad).HasMaxLength(255);
            entity.Property(e => e.Numero).HasMaxLength(100);
            entity.Property(e => e.Provincia).HasMaxLength(255);
            entity.Property(e => e.Hora).HasMaxLength(255);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDeporteNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdDeporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evento_IdDeporte");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evento_IdUsuarioCreador");



        });

        modelBuilder.Entity<FichaBasquet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FichaBas__3214EC07E1E3C3F6");

            entity.ToTable("FichaBasquet");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.FichaBasquets)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FichaBasquet_Usuario");
        });

        modelBuilder.Entity<FichaDeportistum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FichaDep__3214EC07E7BD1CE1");

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
            

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.FichaDeportista)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ficha_Usuario");
        });

        modelBuilder.Entity<FichaFutbol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FichaFut__3214EC07E825038B");

            entity.ToTable("FichaFutbol");
            
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.FichaFutbols)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FichaFutbol_Usuario");
        });

        modelBuilder.Entity<FichaTeni>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FichaTen__3214EC074863C313");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.FichaTenis)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FichaTenis_Usuario");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__03DC48A5B0040F4D");

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
            entity.HasKey(e => e.IdParticipantes).HasName("PK__Particip__229AD721108712B9");

            entity.ToTable("Participante");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Participantes)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Participante_IdEvento");

            entity.HasOne(d => d.IdUsuarioCreadorEventoNavigation).WithMany(p => p.ParticipanteIdUsuarioCreadorEventoNavigations)
                .HasForeignKey(d => d.IdUsuarioCreadorEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Participante_IdCreadorEvento");

            entity.HasOne(d => d.IdUsuarioParticipanteNavigation).WithMany(p => p.ParticipanteIdUsuarioParticipanteNavigations)
                .HasForeignKey(d => d.IdUsuarioParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Participante_IdParticipante");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07D4767CDC");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellido).HasMaxLength(255);
            entity.Property(e => e.Apodo).HasMaxLength(255);
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
            entity.Property(e => e.TokenConfirmacion).HasMaxLength(255);
            entity.Property(e => e.TokenCambioContrasenia).HasMaxLength(255);
        });

    }
}