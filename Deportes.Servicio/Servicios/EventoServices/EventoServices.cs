using Deportes.Modelo.EventoModel;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IUsuario;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.EventoServices;

public class EventoServices: IEventoServices
{
    
    private readonly IEventoRepository _eventoRepository;
    public EventoServices(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
   
    }

    public void AgregarEvento(string nombre, string direccion, string provincia, string localidad, string numero, string hora, int idUsuarioCreador, int idDeporte, DateTime fecha)
    {
       Evento evento= new Evento();
        evento.Nombre=nombre;
        evento.Direccion=direccion;
        evento.Provincia=provincia;
        evento.Localidad=localidad;
        evento.Numero=numero;
        evento.Hora=hora;
        evento.IdDeporte=idDeporte;
        evento.IdUsuarioCreador=idUsuarioCreador;
        evento.Fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day);
        evento.Finalizado = false;
        _eventoRepository.AgregarEvento(evento);
    }
}
