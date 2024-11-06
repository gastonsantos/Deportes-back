﻿// <auto-generated />
using System;
using Deportes.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Deportes.Infra.Migrations
{
    [DbContext(typeof(DeportesContext))]
    [Migration("20241103161223_InitDB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Deportes.Modelo.CalificacionModel.Calificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Calificacion1")
                        .HasColumnType("int")
                        .HasColumnName("calificacion");

                    b.Property<int?>("IdEventoParticipo")
                        .HasColumnType("int");

                    b.Property<int?>("IdUsuarioCalificado")
                        .HasColumnType("int");

                    b.Property<int?>("IdUsuarioCalificador")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Califica__3214EC0786B70E49");

                    b.HasIndex("IdEventoParticipo");

                    b.HasIndex("IdUsuarioCalificado");

                    b.HasIndex("IdUsuarioCalificador");

                    b.ToTable("Calificacion", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.DeporteModel.Deporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CantJugadores")
                        .HasColumnType("int")
                        .HasColumnName("cantJugadores");

                    b.Property<string>("Imagen")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("imagen");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Deporte__3214EC07891614B8");

                    b.ToTable("Deporte", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.EventoModel.Evento", b =>
                {
                    b.Property<int>("IdEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEvento"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("date");

                    b.Property<bool?>("Finalizado")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit")
                        .HasComputedColumnSql("(case when [Fecha] IS NULL then NULL else case when CONVERT([date],[Fecha])<CONVERT([date],getdate()) then CONVERT([bit],(1)) else CONVERT([bit],(0)) end end)", false);

                    b.Property<string>("Hora")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("IdDeporte")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioCreador")
                        .HasColumnType("int");

                    b.Property<string>("Localidad")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Numero")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Provincia")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdEvento")
                        .HasName("PK__Evento__034EFC04E8419F4C");

                    b.HasIndex("IdDeporte");

                    b.HasIndex("IdUsuarioCreador");

                    b.ToTable("Evento", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.FichaBasquetModel.FichaBasquet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Defensa")
                        .HasColumnType("int");

                    b.Property<int?>("Finalizacion")
                        .HasColumnType("int");

                    b.Property<int?>("Fuerza")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int?>("Organizacion")
                        .HasColumnType("int");

                    b.Property<int?>("Tiro")
                        .HasColumnType("int");

                    b.Property<int?>("Velocidad")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__FichaBas__3214EC07E1E3C3F6");

                    b.HasIndex("IdUsuario");

                    b.ToTable("FichaBasquet", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.FichaDeportistaModel.FichaDeportistum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Altura")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Edad")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("ManoHabil")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Peso")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PieHabil")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__FichaDep__3214EC07E7BD1CE1");

                    b.HasIndex("IdUsuario");

                    b.ToTable("FichaDeportista");
                });

            modelBuilder.Entity("Deportes.Modelo.FichaFutbolModel.FichaFutbol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Agilidad")
                        .HasColumnType("int");

                    b.Property<int?>("Fuerza")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int?>("Media")
                        .HasColumnType("int");

                    b.Property<int?>("Precision")
                        .HasColumnType("int");

                    b.Property<int?>("Resistencia")
                        .HasColumnType("int");

                    b.Property<int?>("Tecnica")
                        .HasColumnType("int");

                    b.Property<int?>("Velocidad")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__FichaFut__3214EC07E825038B");

                    b.HasIndex("IdUsuario");

                    b.ToTable("FichaFutbol", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.FichaTenisModel.FichaTeni", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Drive")
                        .HasColumnType("int");

                    b.Property<int?>("Fuerza")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int?>("Reves")
                        .HasColumnType("int");

                    b.Property<int?>("Servicio")
                        .HasColumnType("int");

                    b.Property<int?>("Velocidad")
                        .HasColumnType("int");

                    b.Property<int?>("Volea")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__FichaTen__3214EC074863C313");

                    b.HasIndex("IdUsuario");

                    b.ToTable("FichaTeni");
                });

            modelBuilder.Entity("Deportes.Modelo.HistorialRefreshModel.HistorialRefreshToken", b =>
                {
                    b.Property<int>("IdHistorialToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHistorialToken"));

                    b.Property<bool?>("EsActivo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit")
                        .HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaExpiracion")
                        .HasColumnType("datetime");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Token")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("IdHistorialToken")
                        .HasName("PK__Historia__03DC48A5B0040F4D");

                    b.HasIndex("IdUsuario");

                    b.ToTable("HistorialRefreshToken", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.ParticipanteModel.Participante", b =>
                {
                    b.Property<int>("IdParticipantes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdParticipantes"));

                    b.Property<bool?>("Aceptado")
                        .HasColumnType("bit");

                    b.Property<int>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioCreadorEvento")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioParticipante")
                        .HasColumnType("int");

                    b.Property<bool?>("InvitaEsDuenio")
                        .HasColumnType("bit");

                    b.Property<bool?>("NotificacionVista")
                        .HasColumnType("bit");

                    b.HasKey("IdParticipantes")
                        .HasName("PK__Particip__229AD721108712B9");

                    b.HasIndex("IdEvento");

                    b.HasIndex("IdUsuarioCreadorEvento");

                    b.HasIndex("IdUsuarioParticipante");

                    b.ToTable("Participante", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.ResultadoModel.Resultado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int?>("ResultadoLocal")
                        .HasColumnType("int");

                    b.Property<int?>("ResultadoVisitante")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Resultad__3214EC072CC358C5");

                    b.HasIndex("IdEvento");

                    b.ToTable("Resultado", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.UsuarioModel.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Apodo")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Contrasenia")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Localidad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TokenCambioContrasenia")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TokenConfirmacion")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("VerifyEmail")
                        .HasColumnType("bit");

                    b.HasKey("Id")
                        .HasName("PK__Usuario__3214EC07D4767CDC");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Deportes.Modelo.CalificacionModel.Calificacion", b =>
                {
                    b.HasOne("Deportes.Modelo.EventoModel.Evento", "IdEventoParticipoNavigation")
                        .WithMany("Calificacions")
                        .HasForeignKey("IdEventoParticipo")
                        .HasConstraintName("FK_Calificacion_evento");

                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioCalificadoNavigation")
                        .WithMany("CalificacionIdUsuarioCalificadoNavigations")
                        .HasForeignKey("IdUsuarioCalificado")
                        .HasConstraintName("FK_Calificacion_usuarioCalificado");

                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioCalificadorNavigation")
                        .WithMany("CalificacionIdUsuarioCalificadorNavigations")
                        .HasForeignKey("IdUsuarioCalificador")
                        .HasConstraintName("FK_Calificacion_usuario");

                    b.Navigation("IdEventoParticipoNavigation");

                    b.Navigation("IdUsuarioCalificadoNavigation");

                    b.Navigation("IdUsuarioCalificadorNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.EventoModel.Evento", b =>
                {
                    b.HasOne("Deportes.Modelo.DeporteModel.Deporte", "IdDeporteNavigation")
                        .WithMany("Eventos")
                        .HasForeignKey("IdDeporte")
                        .IsRequired()
                        .HasConstraintName("FK_Evento_IdDeporte");

                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioCreadorNavigation")
                        .WithMany("Eventos")
                        .HasForeignKey("IdUsuarioCreador")
                        .IsRequired()
                        .HasConstraintName("FK_Evento_IdUsuarioCreador");

                    b.Navigation("IdDeporteNavigation");

                    b.Navigation("IdUsuarioCreadorNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.FichaBasquetModel.FichaBasquet", b =>
                {
                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioNavigation")
                        .WithMany("FichaBasquets")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_FichaBasquet_Usuario");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.FichaDeportistaModel.FichaDeportistum", b =>
                {
                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioNavigation")
                        .WithMany("FichaDeportista")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_Ficha_Usuario");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.FichaFutbolModel.FichaFutbol", b =>
                {
                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioNavigation")
                        .WithMany("FichaFutbols")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_FichaFutbol_Usuario");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.FichaTenisModel.FichaTeni", b =>
                {
                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioNavigation")
                        .WithMany("FichaTenis")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_FichaTenis_Usuario");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.HistorialRefreshModel.HistorialRefreshToken", b =>
                {
                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioNavigation")
                        .WithMany("HistorialRefreshTokens")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__Historial__IdUsu__30F848ED");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.ParticipanteModel.Participante", b =>
                {
                    b.HasOne("Deportes.Modelo.EventoModel.Evento", "IdEventoNavigation")
                        .WithMany("Participantes")
                        .HasForeignKey("IdEvento")
                        .IsRequired()
                        .HasConstraintName("FK_Participante_IdEvento");

                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioCreadorEventoNavigation")
                        .WithMany("ParticipanteIdUsuarioCreadorEventoNavigations")
                        .HasForeignKey("IdUsuarioCreadorEvento")
                        .IsRequired()
                        .HasConstraintName("FK_Participante_IdCreadorEvento");

                    b.HasOne("Deportes.Modelo.UsuarioModel.Usuario", "IdUsuarioParticipanteNavigation")
                        .WithMany("ParticipanteIdUsuarioParticipanteNavigations")
                        .HasForeignKey("IdUsuarioParticipante")
                        .IsRequired()
                        .HasConstraintName("FK_Participante_IdParticipante");

                    b.Navigation("IdEventoNavigation");

                    b.Navigation("IdUsuarioCreadorEventoNavigation");

                    b.Navigation("IdUsuarioParticipanteNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.ResultadoModel.Resultado", b =>
                {
                    b.HasOne("Deportes.Modelo.EventoModel.Evento", "IdEventoNavigation")
                        .WithMany("Resultados")
                        .HasForeignKey("IdEvento")
                        .HasConstraintName("FK_Resultado_Evento");

                    b.Navigation("IdEventoNavigation");
                });

            modelBuilder.Entity("Deportes.Modelo.DeporteModel.Deporte", b =>
                {
                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("Deportes.Modelo.EventoModel.Evento", b =>
                {
                    b.Navigation("Calificacions");

                    b.Navigation("Participantes");

                    b.Navigation("Resultados");
                });

            modelBuilder.Entity("Deportes.Modelo.UsuarioModel.Usuario", b =>
                {
                    b.Navigation("CalificacionIdUsuarioCalificadoNavigations");

                    b.Navigation("CalificacionIdUsuarioCalificadorNavigations");

                    b.Navigation("Eventos");

                    b.Navigation("FichaBasquets");

                    b.Navigation("FichaDeportista");

                    b.Navigation("FichaFutbols");

                    b.Navigation("FichaTenis");

                    b.Navigation("HistorialRefreshTokens");

                    b.Navigation("ParticipanteIdUsuarioCreadorEventoNavigations");

                    b.Navigation("ParticipanteIdUsuarioParticipanteNavigations");
                });
#pragma warning restore 612, 618
        }
    }
}