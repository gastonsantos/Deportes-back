namespace Deportes.api.Controllers.Dto
{
    public class DtoUsuarioRegistrar
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Provincia { get; set; } = null!;

        public string Localidad { get; set; } = null!;  

        public string Direccion { get; set; } = null!;  

        public string Numero { get; set; } = null!;  

        public string? Email { get; set; }

        public string? Contrasenia { get; set; }
    }
}
