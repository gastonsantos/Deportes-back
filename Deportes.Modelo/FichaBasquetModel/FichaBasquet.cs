using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.FichaBasquetModel;

public class FichaBasquet
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int? Finalizacion { get; set; }

    public int? Tiro { get; set; }

    public int? Organizacion { get; set; }

    public int? Defensa { get; set; }

    public int? Fuerza { get; set; }

    public int? Velocidad { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

}
