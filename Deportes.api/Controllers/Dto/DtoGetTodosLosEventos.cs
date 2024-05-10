using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Deportes.api.Controllers.Dto
{
    public class DtoGetTodosLosEventos
    {
        [Range(1, 500, ErrorMessage = "Limit must be between 1 and 500")]
        [DefaultValue(50)]
        public int Limit { get; set; } = 50;


        [Range(0, int.MaxValue, ErrorMessage = "Offset must be a non-negative integer")]
        [DefaultValue(0)]
        public int Offset { get; set; } = 0;

        public override string ToString()
        {
            return $"Limit: {Limit}, Offset: {Offset}";
        }
    }
}
