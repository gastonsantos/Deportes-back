using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.UsuarioModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.EventoModel.Dto
{
    public class DtoEventoDeporte
    {
        public int IdEvento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Provincia { get; set; } = null!;
        public string Localidad { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Hora { get; set; } = null!;
        public int IdDeporte { get; set; }
        public DateTime? Fecha { get; set; }
        public string NombreDep { get; set; } = null!;
        public int? CantJugadores { get; set; }
        public int? CantJugadoresAnotados { get; set; }
        public virtual ICollection<DtoUsuario> DtoUsuarios { get; set; } = new List<DtoUsuario>();
        public string? Imagen { get; set; }
        public int  IdUsuarioDuenio { get; set; }
        public string NombreDuenio { get; set; } = null!;
        public int IdParticipante { get; set; }
    }
}
