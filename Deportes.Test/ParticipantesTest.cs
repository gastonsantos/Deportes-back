using Deportes.Infra.Database.UsuarioRepository;
using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IResultado;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.EventoServices;
using Deportes.Servicio.Servicios.ParticipantesServices;
using Deportes.Servicio.Servicios.ParticipantesServices.Errores;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Deportes.Test;

public class ParticipantesTest
{
    public readonly ParticipantesServices _participantesServices;

    private readonly Mock<IEventoRepository> _eventoRepository;
    private readonly Mock<IParticipantesRepository> _participantesRepository;
    private readonly Mock<IDeporteRepository> _deporteRepository;
    private readonly Mock<IUsuarioRepository> _usuarioRepository;
    public ParticipantesTest()
    {
        //con Mock se crearn objetos simulados sin la necesidad de depender de implementaciones reales
        _eventoRepository = new Mock<IEventoRepository>();
        _participantesRepository = new Mock<IParticipantesRepository>();
        _deporteRepository = new Mock<IDeporteRepository>();
        _usuarioRepository= new Mock<IUsuarioRepository>();
          //se instancia el servicio de EventoSercices pasando los objetos simulados como dependencia
          //el .Object devuelve una instancia de la interfaz
          _participantesServices = new ParticipantesServices(_participantesRepository.Object, _eventoRepository.Object, _deporteRepository.Object, _usuarioRepository.Object);
    }

    [Fact]
    public void AceptarParticipanteTest()
    {
        var usuario = new Usuario { Id = 1 };
        var participante = new Participante { IdParticipantes = 1, IdEvento = 1 };
        var evento = new Evento { IdEvento = 1, IdDeporte = 1 };
        var deporte = new Deporte { Id = 1, CantJugadores = 10 };

        _usuarioRepository.Setup(repo => repo.ObtenerUsuarioPorId(It.IsAny<int>())).Returns(usuario);
        _participantesRepository.Setup(repo => repo.ObtenerParticipantePorIdParticipante(It.IsAny<int>())).Returns(participante);
        _eventoRepository.Setup(repo => repo.GetEvento(It.IsAny<int>())).Returns(evento);
        _deporteRepository.Setup(repo => repo.GetDeportePorId(It.IsAny<int>())).Returns(deporte);
        _eventoRepository.Setup(repo => repo.CantidadDeParticipantesEnUnEvento(It.IsAny<int>())).Returns(5);

        _participantesServices.AceptarParticipante(1);

        _participantesRepository.Verify(repo => repo.AceptarParticipante(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void NoPuedeAceptarParticipanteEventoLenoTest()
    {
        var usuario = new Usuario { Id = 1 };
        var participante = new Participante { IdParticipantes = 1, IdEvento = 1 };
        var evento = new Evento { IdEvento = 1, IdDeporte = 1 };
        var deporte = new Deporte { Id = 1, CantJugadores = 10 };

        _usuarioRepository.Setup(repo => repo.ObtenerUsuarioPorId(It.IsAny<int>())).Returns(usuario);
        _participantesRepository.Setup(repo => repo.ObtenerParticipantePorIdParticipante(It.IsAny<int>())).Returns(participante);
        _eventoRepository.Setup(repo => repo.GetEvento(It.IsAny<int>())).Returns(evento);
        _deporteRepository.Setup(repo => repo.GetDeportePorId(It.IsAny<int>())).Returns(deporte);
        //le estoy pasando que la cantidad de participantes es 10 asi deveuvle un false
        _eventoRepository.Setup(repo => repo.CantidadDeParticipantesEnUnEvento(It.IsAny<int>())).Returns(10);

        Assert.Throws<EventoLlenoException>(() => _participantesServices.AceptarParticipante(1));
        _participantesRepository.Verify(repo => repo.EliminarParticipante(It.IsAny<int>()), Times.Once);
    }
    [Fact]
    public void PuedeEliminarParticipanteTest()
    {
        var usuario = new Usuario { Id = 1 };
        var participante = new Participante { IdParticipantes = 1, IdEvento = 1 };
        var evento = new Evento { IdEvento = 1, IdDeporte = 1 };
        var deporte = new Deporte { Id = 1, CantJugadores = 10 };

        _usuarioRepository.Setup(repo => repo.ObtenerUsuarioPorId(It.IsAny<int>())).Returns(usuario);
        _participantesServices.EliminarParticipante(It.IsAny<int>());
        _participantesRepository.Verify(repo => repo.EliminarParticipante(It.IsAny<int>()), Times.Once);
    }
    [Fact]
    public void EnviarNotificacionParticipanteTest()
    {
        var usuario = new Usuario { Id = 1 };
        var participante = new Participante { IdParticipantes = 1, IdEvento = 1,IdUsuarioCreadorEvento=3 };
        var evento = new Evento { IdEvento = 1, IdDeporte = 1 ,IdUsuarioCreador=3};
        var deporte = new Deporte { Id = 1, CantJugadores = 10 };

        _eventoRepository.Setup(r => r.GetEvento(It.IsAny<int>())).Returns(evento);
        _usuarioRepository.Setup(repo => repo.ObtenerUsuarioPorId(It.IsAny<int>())).Returns(usuario);
        _participantesRepository.Setup(r => r.YaHayNotificacion(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        _participantesServices.EnviarNotificacionParticipante(1, 2, 3);
        _participantesRepository.Verify(repo => repo.EnviarNotificacionParticipante(1, 2, 3, false), Times.Once);
    }

    [Fact]
    public void NoPuedeEnviarNotificacionPorqueNoSePuedenEnviarInvitacionesAlMismoUsuarioExceptionTest()
    {
        var usuario = new Usuario { Id = 1 };
        var participante = new Participante { IdParticipantes = 1, IdEvento = 1, IdUsuarioCreadorEvento = 3 };
        var evento = new Evento { IdEvento = 1, IdDeporte = 1, IdUsuarioCreador = 3 };
        var deporte = new Deporte { Id = 1, CantJugadores = 10 };



        _eventoRepository.Setup(r => r.GetEvento(It.IsAny<int>())).Returns(evento);
        _usuarioRepository.Setup(repo => repo.ObtenerUsuarioPorId(It.IsAny<int>())).Returns(usuario);
        _participantesRepository.Setup(r => r.YaHayNotificacion(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);
       
        Assert.Throws<NoSePuedenEnviarInvitacionesAlMismoUsuario>(() => _participantesServices.EnviarNotificacionParticipante(evento.IdEvento, usuario.Id, usuario.Id));
        
    }
}
