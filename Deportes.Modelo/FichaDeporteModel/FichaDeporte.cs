using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.FichaDeporteModel;

public class FichaDeporte
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdDeporte { get; set; }

    public string? Avatar { get; set; }

    public string? Edad { get; set; }

    public string? Altura { get; set; }

    public string? Peso { get; set; }

    public string? PieHabil { get; set; }

    public string? ManoHabil { get; set; }

    public string? Posicion { get; set; }

    public virtual Deporte IdDeporteNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
