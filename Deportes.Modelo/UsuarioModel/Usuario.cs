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
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; } 
        public string Telefono { get; set; }
        public DateTime Fecha_nacimiento { get; set; }



    }
}
