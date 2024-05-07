using Deportes.Modelo.CalificacionModel;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.FichaBasquetModel;
using Deportes.Modelo.FichaDeporteModel;
using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
using Deportes.Modelo.FichaTenisModel;
using Deportes.Modelo.HistorialRefreshModel;
using Deportes.Modelo.ParticipanteModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.UsuarioModel
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string? Apodo { get; set; }

        public string? Email { get; set; }

        public string? Contrasenia { get; set; }

        public string Provincia { get; set; } = null!;

        public string Localidad { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Numero { get; set; } = null!;

        public bool? VerifyEmail { get; set; }

        public bool? Activo { get; set; }

        public string? TokenCambioContrasenia { get; set; }

        public string? TokenConfirmacion { get; set; }

        public virtual ICollection<Calificacion> CalificacionIdUsuarioCalificadoNavigations { get; set; } = new List<Calificacion>();

        public virtual ICollection<Calificacion> CalificacionIdUsuarioCalificadorNavigations { get; set; } = new List<Calificacion>();

        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

        public virtual ICollection<FichaBasquet> FichaBasquets { get; set; } = new List<FichaBasquet>();

        public virtual ICollection<FichaDeportistum> FichaDeportista { get; set; } = new List<FichaDeportistum>();

        public virtual ICollection<FichaFutbol> FichaFutbols { get; set; } = new List<FichaFutbol>();

        public virtual ICollection<FichaTeni> FichaTenis { get; set; } = new List<FichaTeni>();

        public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; set; } = new List<HistorialRefreshToken>();

        public virtual ICollection<Participante> ParticipanteIdUsuarioCreadorEventoNavigations { get; set; } = new List<Participante>();

        public virtual ICollection<Participante> ParticipanteIdUsuarioParticipanteNavigations { get; set; } = new List<Participante>();

    }
}
