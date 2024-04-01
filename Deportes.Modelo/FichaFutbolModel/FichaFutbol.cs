using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.FichaFutbolModel
{
    public class FichaFutbol
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int? Velocidad { get; set; }

        public int? Disparo { get; set; }

        public int? Regate { get; set; }

        public int? Fuerza { get; set; }

        public int? Pase { get; set; }

        public int? Defensa { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

     
    }
}
