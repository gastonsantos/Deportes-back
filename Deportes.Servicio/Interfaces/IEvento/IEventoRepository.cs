using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IEvento
{
    public interface IEventoRepository
    {
        public void AgregarEvento(Evento evento);
        public IList<DtoEventoDeporte> GetAllEventosConDeportes();
        public IList<DtoEventoDeporte> GetEventosCreadosPorUsuario(int idUsuario);
        public DtoEventoDeporte GetEventoConDeporte(int idEvento);
        public Evento GetEvento(int idEvento);
        public void ModificarEvento(Evento evento);
        public void CambiarEstadoEvento(int idEvento);
        public int IdUsuarioCreadorPorIdEvento(int idEvento);
        public IList<DtoEventoDeporte> GetEventosEnLosQueParticipo(int idUsuario);
    }

}
