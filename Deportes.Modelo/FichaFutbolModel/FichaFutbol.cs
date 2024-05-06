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

        public int? Resistencia { get; set; }

        public int? Precision { get; set; }

        public int? Fuerza { get; set; }

        public int? Tecnica { get; set; }

        public int? Agilidad { get; set; }


        public int? Media { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

     
    }
}
