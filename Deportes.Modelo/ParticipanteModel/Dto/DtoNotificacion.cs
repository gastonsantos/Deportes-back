using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.ParticipanteModel.Dto;

public class DtoNotificacion
{
    public int IdParticipantes { get; set; }

    public int IdEvento { get; set; }
    public int IdElQueInvita { get; set; }
    public bool? Aceptado { get; set; }

    public bool? InvitaEsDuenio { get; set; }
    public string? NombreUsuarioInvito { get; set; }

    public string ApellidoUsuarioInvito { get; set; }

    public string? NombreDeporte { get; set; }

    public string Hora { get; set; }
    public DateTime? Fecha { get; set; }

    public string Provincia { get; set; } = null!;

    public string Localidad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Numero { get; set; } = null!;

}
