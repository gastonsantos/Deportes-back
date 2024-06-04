using Deportes.Infra.Database.EventoRepository;
using Deportes.Infra.Database.FichaRepository;
using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Modelo.UsuarioModel.Dto;
using Deportes.Servicio.Interfaces.ICorreo;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.ParticipantesServices;
using Deportes.Servicio.Servicios.UsuarioServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Deportes.Test;

public class UsuarioTest
{
    public readonly UsuarioService _usuariosServices;

    private readonly Mock<ICorreoServices> _correoServices;
    private readonly Mock<IFichaRepository> _fichaRepository;
    private readonly Mock<IUsuarioRepository> _usuarioRepository;
    public UsuarioTest()
    {
        //con Mock se crearn objetos simulados sin la necesidad de depender de implementaciones reales
        _correoServices = new Mock<ICorreoServices>();
        _fichaRepository = new Mock<IFichaRepository>();
        
        _usuarioRepository = new Mock<IUsuarioRepository>();
        
        _usuariosServices = new UsuarioService(_usuarioRepository.Object, _correoServices.Object, _fichaRepository.Object);
    }

    [Fact]
    public void GetUsuarioParaPerfilInvitacionTest()
    {
        var id = 1;
        var usuario = new Usuario { Id = 1 };
        DtoUsuario dtoUsuario = new DtoUsuario();
        _usuarioRepository.Setup(r => r.ObtenerUsuarioPorId(1)).Returns(new Usuario());
        var Dtousuario =  _usuariosServices.GetUsuarioParaPerfilInvitacion(id);
        Assert.NotNull(dtoUsuario);
    }
    [Fact]
    public void GuardarUsuarioEnBdTest()
    {
        string nombre = "Gaston";
        string apellido = "Santos";
        string apodo="Tonga";
        string email = "123@mail.com";
        string contrasenia = "12345";
        string provincia = "Buenos Aires";
        string localidad = "Laferrere";
        string direccion = "Martin Coronado";
        string numero = "3323";


        Usuario usaurio = new Usuario();

        DtoUsuario dtoUsuario = new DtoUsuario();
     
        _usuarioRepository.Setup(r => r.ObtenerUsuarioPorEmail(email)).Returns((Usuario)null);
        _usuariosServices.GuardarUsuarioEnBd(nombre, apellido, apodo, email, contrasenia, provincia, localidad, direccion, numero);
        var idUsuario= _usuarioRepository.Setup(r => r.GuardarUsuarioEnBd(usaurio)).Returns(1);
        Assert.NotNull(idUsuario);

    }


}
