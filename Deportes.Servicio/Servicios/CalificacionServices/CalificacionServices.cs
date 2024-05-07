using Deportes.Modelo.CalificacionModel;
using Deportes.Servicio.Interfaces.ICalificacion;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Servicios.CalificacionServices.Dto;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.CalificacionServices;

public class CalificacionServices : ICalificacionServices
{
    private readonly ICalificacionRepository _calificacionRepository;

    public CalificacionServices(ICalificacionRepository calificacionRepository)
    {
        _calificacionRepository = calificacionRepository;


    }

    public void Calificar(DtoCalificacion calificacionDto)
    {
        Calificacion calificacion = new Calificacion();
        calificacion.IdUsuarioCalificador = calificacionDto.IdUsuarioCalificador;
        calificacion.IdUsuarioCalificado = calificacionDto.IdUsuarioCalificado;
        calificacion.IdEventoParticipo = calificacionDto.IdEvento;
        calificacion.Calificacion1 = calificacionDto.Calificacion;

        _calificacionRepository.Calificar(calificacion);
    }

    public int? CalificacionPorUsuario(int idUsuario)
    {
        var calificaciones = _calificacionRepository.CalificacionPorUsuario(idUsuario);
        int contados = calificaciones.Count();

        int? suma = 0;

        foreach (var calificacion in calificaciones)
        {
            suma += calificacion.Calificacion1;
        }
        int? resultado = suma/contados;

        return resultado;
    }
}
