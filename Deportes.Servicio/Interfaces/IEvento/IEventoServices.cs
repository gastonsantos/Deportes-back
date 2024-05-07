using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IEvento
{
    public interface IEventoServices
    {
        public void AgregarEvento(string nombre, string provincia, string localidad, string direccion, string numero, string hora, int idUsuarioCreador, int idDeporte, DateTime fecha);

        public IList<DtoEventoDeporte> GetAllEventosConDeportes();
        public IList<DtoEventoDeporte> GetEventosCreadosPorUsuario(int idUsuario);
        public DtoEventoDeporte GetEventoConDeporte(int idEvento);
        public Evento GetEvento(int idEvento);
        public void ModificarEvento(int idEvento, string nombre, string provincia, string localidad, string direccion, string numero, string hora, int idUsuarioCreador, int idDeporte, DateTime fecha);
        public void CambiarEstadoEvento(int idEvento);
        public IList<DtoEventoDeporte> GetEventosEnLosQueParticipo(int idUsuario);
        public IList<DtoEventoDeporte> GetEventosCreadosPorUsuarioFinalizado(int idUsuario);

        public IList<DtoEventoDeporte> GetEventosEnLosQueParticipoFinalizado(int idUsuario);
    }
}
