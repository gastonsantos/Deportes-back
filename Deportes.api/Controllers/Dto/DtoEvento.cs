namespace Deportes.api.Controllers.Dto
{
    public class DtoEvento
    {
        public string Nombre { get; set; } = null!;

        public string Provincia { get; set; } = null!;

        public string Localidad { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Numero { get; set; } = null!;
        public string Hora { get; set; } = null!;
        public int IdUsuarioCreador { get; set; }

        public int IdDeporte { get; set; }

        public DateTime Fecha { get; set; }
    }
}
