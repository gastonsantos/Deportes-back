using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.ComunModel;

public enum CategoriaError : short
{
    EntidadSinEncontrar,
    EntidadEncontrada,
    NoAutorizado,
    NoAutenticado,
    ReglaNegocioNoCumplida,
    SinEspecificar

}
