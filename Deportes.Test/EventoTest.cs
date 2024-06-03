using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IResultado;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.EventoServices;
using Deportes.Servicio.Servicios.EventoServices.Errores;
using Moq;
using Xunit;

namespace Deportes.Test;

public class EventoTest
{
    public readonly EventoServices _eventoServices;

    private readonly Mock<IEventoRepository> _eventoRepository;
    private readonly Mock<IParticipantesRepository> _participantesRepository;
    private readonly Mock<IUsuarioRepository> _usuariosRepository;
    private readonly Mock<IDeporteRepository> _deporteRepository;
    private readonly Mock<IResultadoRepository> _resultadoRepository;
    
    public EventoTest()
    {
        //con Mock se crearn objetos simulados sin la necesidad de depender de implementaciones reales
        _eventoRepository= new Mock<IEventoRepository>();
        _participantesRepository = new Mock<IParticipantesRepository>();
        _usuariosRepository= new Mock<IUsuarioRepository>();
        _deporteRepository= new Mock<IDeporteRepository>();
        _resultadoRepository= new Mock<IResultadoRepository>();
        //se instancia el servicio de EventoSercices pasando los objetos simulados como dependencia
        //el .Object devuelve una instancia de la interfaz
        _eventoServices = new EventoServices(_eventoRepository.Object,_participantesRepository.Object, _usuariosRepository.Object, _deporteRepository.Object, _resultadoRepository.Object);
    }

    [Fact]
    public void GetEventoTest()
    {
        int eventoId = 1;
        var esperadoEvento = new Evento { IdEvento = eventoId, Nombre = "Futbol" };
        // Configuración del mock: Usa el método Setup del mock _eventoRepository para especificar que cuando se llame al método GetEvento con el eventoId de 1,
        // el mock debe retornar el objeto esperadoEvento.
        _eventoRepository.Setup(r => r.GetEvento(eventoId)).Returns(esperadoEvento);

        var actualEvento = _eventoServices.GetEvento(eventoId);

        Assert.NotNull(actualEvento);
        Assert.Equal(esperadoEvento.IdEvento, actualEvento.IdEvento);
        Assert.Equal(esperadoEvento.Nombre, actualEvento.Nombre);


    }


    [Fact]
    public void GetEventosCreadoPorUsuarioTest()
    {
        int idUsuario = 3;
        int eventoId = 1;
        int eventoId2 = 2;
        var esperadoEvento = new DtoEventoDeporte { IdEvento = eventoId, Nombre = "Futbol" };
        var esperadoEvento2= new DtoEventoDeporte { IdEvento = eventoId2, Nombre = "Basquets" };

        List<DtoEventoDeporte> eventosEsperados = new List<DtoEventoDeporte>();
        eventosEsperados.Add(esperadoEvento);
        eventosEsperados.Add(esperadoEvento2);
        // Configuración del mock: Usa el método Setup del mock _eventoRepository para especificar que cuando se llame al método GetEvento con el eventoId de 1,
        // el mock debe retornar el objeto esperadoEvento.
        _eventoRepository.Setup(r => r.GetEventosCreadosPorUsuario(idUsuario)).Returns(eventosEsperados);

        List<DtoEventoDeporte> eventosActuales = (List<DtoEventoDeporte>)_eventoServices.GetEventosCreadosPorUsuario(idUsuario);
        int cantidad = eventosActuales.Count();
        Assert.NotNull(eventosActuales);

        int cantEventosEsperado = 0;

        foreach(DtoEventoDeporte evento in eventosEsperados)
        {
            cantEventosEsperado++;
            
        }
        Assert.Equal(cantEventosEsperado, cantidad);
    }
    [Fact]
    public void TestQueAgregaUnEventoCorrectamente()
    {
        // Arrange
        var nombre = "nombre";
        var provincia = "provincia";
        var localidad = "localidad";
        var direccion = "direccion";
        var numero = "numero";
        var hora = "hora";
        var idUsuarioCreador = 1;
        var idDeporte = 1;
        var fecha = DateTime.Now.AddDays(1);

        //Retorna los valores de las validaciones no las validaciones 
        _usuariosRepository.Setup(x => x.ObtenerUsuarioPorId(idUsuarioCreador)).Returns(new Usuario());
        _deporteRepository.Setup(x => x.GetDeportePorId(idDeporte)).Returns(new Deporte());
        _eventoRepository.Setup(x => x.AgregarEvento(It.IsAny<Evento>())).Returns(1);

        // Act
        _eventoServices.AgregarEvento(nombre, provincia, localidad, direccion, numero, hora, idUsuarioCreador, idDeporte, fecha);

        // Assert
        //Verify, Times.Once === Verifica que el Metodo se haya ejecutado una sola vez, para confirmar su correcto funcionamiento
        // Esto se usa en los metodos void  porque no hay forma de verificar ya que no devuelve nada
        // It.IsAny<T>() === Lo que indica es que no importa lo que Sea T sino que se ejecute.
        _eventoRepository.Verify(x => x.AgregarEvento(It.IsAny<Evento>()), Times.Once);
    }


    [Fact]
    public void AgregarEvento_ExepcionDeporteNoexiste()
    {
        // Arrange
        var nombre = "nombre";
        var provincia = "provincia";
        var localidad = "localidad";
        var direccion = "direccion";
        var numero = "numero";
        var hora = "hora";
        var idUsuarioCreador = 1;
        var idDeporte = 1;
        var fecha = DateTime.Now.AddDays(1);

        _usuariosRepository.Setup(x => x.ObtenerUsuarioPorId(idUsuarioCreador)).Returns(new Usuario());
        _deporteRepository.Setup(x => x.GetDeportePorId(idDeporte)).Returns((Deporte)null);
        _eventoRepository.Setup(x => x.AgregarEvento(It.IsAny<Evento>())).Returns(1);
        // Act & Assert
        // lo que hace es que salta la Excepcion
        Assert.Throws<ErrorEnEventoException>(() =>
            _eventoServices.AgregarEvento(nombre, provincia, localidad, direccion, numero, hora, idUsuarioCreador, idDeporte, fecha));
    }
    [Fact]
    public void AgregarEvento_ExepcionNombreEsNullOrEmpty()
    {
        // Arrange
        var nombre ="";
        var provincia = "provincia";
        var localidad = "localidad";
        var direccion = "direccion";
        var numero = "numero";
        var hora = "hora";
        var idUsuarioCreador = 1;
        var idDeporte = 1;
        var fecha = DateTime.Now.AddDays(1);

        _usuariosRepository.Setup(x => x.ObtenerUsuarioPorId(idUsuarioCreador)).Returns(new Usuario());
        _deporteRepository.Setup(x => x.GetDeportePorId(idDeporte)).Returns(new Deporte());
        _eventoRepository.Setup(x => x.AgregarEvento(It.IsAny<Evento>())).Returns(1);
        // Act & Assert
        Assert.Throws<ErrorEnEventoException>(() =>
            _eventoServices.AgregarEvento(nombre, provincia, localidad, direccion, numero, hora, idUsuarioCreador, idDeporte, fecha));
    }
    [Fact]
    public void AgregarEvento_ExepcionFechaInvalida()
    {
        // Arrange
        var nombre = "pelota";
        var provincia = "provincia";
        var localidad = "localidad";
        var direccion = "direccion";
        var numero = "numero";
        var hora = "hora";
        var idUsuarioCreador = 1;
        var idDeporte = 1;
        var fecha = DateTime.Now.AddDays(-1);

        _usuariosRepository.Setup(x => x.ObtenerUsuarioPorId(idUsuarioCreador)).Returns(new Usuario());
        _deporteRepository.Setup(x => x.GetDeportePorId(idDeporte)).Returns(new Deporte());
        _eventoRepository.Setup(x => x.AgregarEvento(It.IsAny<Evento>())).Returns(1);
        // Act & Assert
        Assert.Throws<ErrorEnEventoException>(() =>
            _eventoServices.AgregarEvento(nombre, provincia, localidad, direccion, numero, hora, idUsuarioCreador, idDeporte, fecha));
    }
    [Fact]
    public void ModificarEventoExitosamente()
    {
        // Arrange
        var idUsuario = 1;
        var nombre = "pelota";
        var provincia = "provincia";
        var localidad = "localidad";
        var direccion = "direccion";
        var numero = "numero";
        var hora = "hora";
        var idUsuarioCreador = 1;
        var idDeporte = 1;
        var fecha = DateTime.Now.AddDays(1);

        _eventoRepository.Setup(x => x.GetEvento(idUsuario)).Returns(new Evento());
        _usuariosRepository.Setup(x => x.ObtenerUsuarioPorId(idUsuarioCreador)).Returns(new Usuario());
        _deporteRepository.Setup(x => x.GetDeportePorId(idDeporte)).Returns(new Deporte());
        

        _eventoServices.ModificarEvento(idUsuario, nombre, provincia, localidad, direccion, numero, hora, idUsuarioCreador, idDeporte, fecha);

        _eventoRepository.Verify(x => x.ModificarEvento(It.IsAny<Evento>()), Times.Once);
    }
    [Fact]
    public void GetEventosCreadosPorUsuarioFinalizadoConOffsetAndLimitTest()
    {


        // Arrange
        int idUsuario = 1;
        int limit = 5;
        int offset = 0;
        //Preparo la lista de eventos

        int eventoId = 1;
        int eventoId2 = 2;
        int eventoId3 = 3;
        int eventoId4 = 4;
        int eventoId5 = 5;
        int eventoId6 = 6;
        var esperadoEvento = new DtoEventoDeporte { IdEvento = eventoId, Nombre = "Futbol" };
        var esperadoEvento2 = new DtoEventoDeporte { IdEvento = eventoId2, Nombre = "Basquets" };
        var esperadoEvento3 = new DtoEventoDeporte { IdEvento = eventoId3, Nombre = "Basts" };
        var esperadoEvento4 = new DtoEventoDeporte { IdEvento = eventoId4, Nombre = "Basquts" };
        var esperadoEvento5 = new DtoEventoDeporte { IdEvento = eventoId5, Nombre = "Basqus" };
        var esperadoEvento6 = new DtoEventoDeporte { IdEvento = eventoId6, Nombre = "Basquet" };
        List<DtoEventoDeporte> eventosEsperados = new List<DtoEventoDeporte>();
        eventosEsperados.Add(esperadoEvento);
        eventosEsperados.Add(esperadoEvento2);
        eventosEsperados.Add(esperadoEvento3);
        eventosEsperados.Add(esperadoEvento4);
        eventosEsperados.Add(esperadoEvento5);
        eventosEsperados.Add(esperadoEvento6);


        _eventoRepository.Setup(x => x.GetEventosCreadosPorUsuarioFinalizado(idUsuario)).Returns(eventosEsperados);
        _usuariosRepository.Setup(x => x.ObtenerUsuarioPorId(idUsuario)).Returns(new Usuario());
        var eventos =  _eventoServices.GetEventosCreadosPorUsuarioFinalizado(idUsuario, limit, offset);

        int cantEventosEsperado = 0;

        foreach (var lista in eventos)
        {
            
            cantEventosEsperado++;


        }

        Assert.Equal(limit, cantEventosEsperado);
    }

    [Fact]
    public void CambiaEstadoDelEventoTest()
    {


        // Arrange
        int idEvento = 1;
     
        _eventoRepository.Setup(x => x.GetEvento(idEvento)).Returns(new Evento());

        _eventoServices.CambiarEstadoEvento(idEvento);

        _eventoRepository.Verify(x => x.CambiarEstadoEvento(idEvento), Times.Once);
       
    }
    [Fact]
    public void CambiaEstadoDelEventoQueNoEncuentraEventoExceptionTest()
    {


        // Arrange
        int idEvento = 1;
      
        _eventoRepository.Setup(x => x.GetEvento(idEvento)).Returns((Evento)null); ;

        //_eventoServices.CambiarEstadoEvento(idEvento);

        Assert.Throws<EventoNoEncontradoException>(() =>
           _eventoServices.CambiarEstadoEvento(idEvento));

    }
}