using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.UsuarioServices;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService (IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public IList<Usuario> GetAll()
    {
       return _usuarioRepository.GetAll();
    }

    public Usuario? ObtenerUsuarioMailContraseña(string email, string contra)
    {   

        return _usuarioRepository.ObtenerUsuarioMailContraseña(email, contra);
    }

    public Usuario? ObtenerUsuarioPorId(int id)
    {
        return _usuarioRepository.ObtenerUsuarioPorId(id);
    }
}
