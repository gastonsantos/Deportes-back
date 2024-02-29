using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.DeporteModel;

public class Deporte
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int CantJugadores { get; set; }
    public string Imagen { get; set; }
    

}
