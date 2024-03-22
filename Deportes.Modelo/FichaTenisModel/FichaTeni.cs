using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.FichaTenisModel;

public class FichaTeni
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int? Servicio { get; set; }

    public int? Drive { get; set; }

    public int? Reves { get; set; }

    public int? Volea { get; set; }

    public int? Fuerza { get; set; }

    public int? Velocidad { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
