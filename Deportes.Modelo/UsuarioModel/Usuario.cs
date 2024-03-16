using Deportes.Modelo.EventoModel;
using Deportes.Modelo.FichaDeporteModel;
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

        public string? Email { get; set; }

        public string? Contrasenia { get; set; }

        public string Provincia { get; set; } = null!;

        public string Localidad { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Numero { get; set; } = null!;

        public string? TokenConfirmacion { get; set; }

        public string? TokenCambioContrasenia { get; set; }
        public bool? VerifyEmail { get; set; }

        public bool? Activo { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

        public virtual ICollection<FichaDeporte> FichaDeportes { get; set; } = new List<FichaDeporte>();

        public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; set; } = new List<HistorialRefreshToken>();

        public virtual ICollection<Participante> ParticipanteIdUsuarioCreadorEventoNavigations { get; set; } = new List<Participante>();

        public virtual ICollection<Participante> ParticipanteIdUsuarioParticipanteNavigations { get; set; } = new List<Participante>();

    }
}
