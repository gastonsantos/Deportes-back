using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deportes.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    cantJugadores = table.Column<int>(type: "int", nullable: true),
                    imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Deporte__3214EC07891614B8", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Apodo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Contrasenia = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VerifyEmail = table.Column<bool>(type: "bit", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true),
                    TokenCambioContrasenia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TokenConfirmacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3214EC07D4767CDC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    IdEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    IdUsuarioCreador = table.Column<int>(type: "int", nullable: false),
                    IdDeporte = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Hora = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Finalizado = table.Column<bool>(type: "bit", nullable: true, computedColumnSql: "(case when [Fecha] IS NULL then NULL else case when CONVERT([date],[Fecha])<CONVERT([date],getdate()) then CONVERT([bit],(1)) else CONVERT([bit],(0)) end end)", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Evento__034EFC04E8419F4C", x => x.IdEvento);
                    table.ForeignKey(
                        name: "FK_Evento_IdDeporte",
                        column: x => x.IdDeporte,
                        principalTable: "Deporte",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evento_IdUsuarioCreador",
                        column: x => x.IdUsuarioCreador,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FichaBasquet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Finalizacion = table.Column<int>(type: "int", nullable: true),
                    Tiro = table.Column<int>(type: "int", nullable: true),
                    Organizacion = table.Column<int>(type: "int", nullable: true),
                    Defensa = table.Column<int>(type: "int", nullable: true),
                    Fuerza = table.Column<int>(type: "int", nullable: true),
                    Velocidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FichaBas__3214EC07E1E3C3F6", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaBasquet_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FichaDeportista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Edad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Altura = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Peso = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PieHabil = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ManoHabil = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FichaDep__3214EC07E7BD1CE1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ficha_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FichaFutbol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Velocidad = table.Column<int>(type: "int", nullable: true),
                    Resistencia = table.Column<int>(type: "int", nullable: true),
                    Precision = table.Column<int>(type: "int", nullable: true),
                    Fuerza = table.Column<int>(type: "int", nullable: true),
                    Tecnica = table.Column<int>(type: "int", nullable: true),
                    Agilidad = table.Column<int>(type: "int", nullable: true),
                    Media = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FichaFut__3214EC07E825038B", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaFutbol_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FichaTeni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Servicio = table.Column<int>(type: "int", nullable: true),
                    Drive = table.Column<int>(type: "int", nullable: true),
                    Reves = table.Column<int>(type: "int", nullable: true),
                    Volea = table.Column<int>(type: "int", nullable: true),
                    Fuerza = table.Column<int>(type: "int", nullable: true),
                    Velocidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FichaTen__3214EC074863C313", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaTenis_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistorialRefreshToken",
                columns: table => new
                {
                    IdHistorialToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime", nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: true, computedColumnSql: "(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__03DC48A5B0040F4D", x => x.IdHistorialToken);
                    table.ForeignKey(
                        name: "FK__Historial__IdUsu__30F848ED",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Calificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCalificador = table.Column<int>(type: "int", nullable: true),
                    IdUsuarioCalificado = table.Column<int>(type: "int", nullable: true),
                    IdEventoParticipo = table.Column<int>(type: "int", nullable: true),
                    calificacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Califica__3214EC0786B70E49", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificacion_evento",
                        column: x => x.IdEventoParticipo,
                        principalTable: "Evento",
                        principalColumn: "IdEvento");
                    table.ForeignKey(
                        name: "FK_Calificacion_usuario",
                        column: x => x.IdUsuarioCalificador,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calificacion_usuarioCalificado",
                        column: x => x.IdUsuarioCalificado,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Participante",
                columns: table => new
                {
                    IdParticipantes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEvento = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioCreadorEvento = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioParticipante = table.Column<int>(type: "int", nullable: false),
                    Aceptado = table.Column<bool>(type: "bit", nullable: true),
                    NotificacionVista = table.Column<bool>(type: "bit", nullable: true),
                    InvitaEsDuenio = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Particip__229AD721108712B9", x => x.IdParticipantes);
                    table.ForeignKey(
                        name: "FK_Participante_IdCreadorEvento",
                        column: x => x.IdUsuarioCreadorEvento,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participante_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Evento",
                        principalColumn: "IdEvento");
                    table.ForeignKey(
                        name: "FK_Participante_IdParticipante",
                        column: x => x.IdUsuarioParticipante,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resultado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEvento = table.Column<int>(type: "int", nullable: true),
                    ResultadoLocal = table.Column<int>(type: "int", nullable: true),
                    ResultadoVisitante = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resultad__3214EC072CC358C5", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resultado_Evento",
                        column: x => x.IdEvento,
                        principalTable: "Evento",
                        principalColumn: "IdEvento");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_IdEventoParticipo",
                table: "Calificacion",
                column: "IdEventoParticipo");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_IdUsuarioCalificado",
                table: "Calificacion",
                column: "IdUsuarioCalificado");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_IdUsuarioCalificador",
                table: "Calificacion",
                column: "IdUsuarioCalificador");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdDeporte",
                table: "Evento",
                column: "IdDeporte");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdUsuarioCreador",
                table: "Evento",
                column: "IdUsuarioCreador");

            migrationBuilder.CreateIndex(
                name: "IX_FichaBasquet_IdUsuario",
                table: "FichaBasquet",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_FichaDeportista_IdUsuario",
                table: "FichaDeportista",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_FichaFutbol_IdUsuario",
                table: "FichaFutbol",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_FichaTeni_IdUsuario",
                table: "FichaTeni",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialRefreshToken_IdUsuario",
                table: "HistorialRefreshToken",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Participante_IdEvento",
                table: "Participante",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Participante_IdUsuarioCreadorEvento",
                table: "Participante",
                column: "IdUsuarioCreadorEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Participante_IdUsuarioParticipante",
                table: "Participante",
                column: "IdUsuarioParticipante");

            migrationBuilder.CreateIndex(
                name: "IX_Resultado_IdEvento",
                table: "Resultado",
                column: "IdEvento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificacion");

            migrationBuilder.DropTable(
                name: "FichaBasquet");

            migrationBuilder.DropTable(
                name: "FichaDeportista");

            migrationBuilder.DropTable(
                name: "FichaFutbol");

            migrationBuilder.DropTable(
                name: "FichaTeni");

            migrationBuilder.DropTable(
                name: "HistorialRefreshToken");

            migrationBuilder.DropTable(
                name: "Participante");

            migrationBuilder.DropTable(
                name: "Resultado");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Deporte");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
