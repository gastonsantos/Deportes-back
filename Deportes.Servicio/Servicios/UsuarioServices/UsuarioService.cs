using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.UsuarioServices.Errores;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
        var usuario = _usuarioRepository.ObtenerUsuarioPorId(id);
        if(usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        else
        {
            return _usuarioRepository.ObtenerUsuarioPorId(id);
        }
       
    }

    public void GuardarUsuarioEnBd(string nombre, string apellido, string email, string contrasenia, string provincia, string localidad, string direccion, string numero)
    {
        Usuario usuario = new Usuario();
        usuario.Nombre = nombre;
        usuario.Apellido = apellido;
        usuario.Email = email;
        usuario.Contrasenia = contrasenia;
        usuario.Provincia= provincia;
        usuario.Localidad= localidad;
        usuario.Direccion = direccion;
        usuario.Numero= numero;
        usuario.Activo = true;
        usuario.VerifyEmail = false;

        //_usuarioRepository.GuardarUsuarioEnBd(usuario);

        
        if (usuario != null && _usuarioRepository.ObtenerUsuarioPorEmail(email) == null && EmailConForamtoValido(email))
        {
            _usuarioRepository.GuardarUsuarioEnBd(usuario);
        }
        else
        {
            throw new Exception();
        }
     
    }

    private bool EmailConForamtoValido(string email)
    {

        // Patrón de expresión regular para validar el formato de un correo electrónico
        string patron = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

        // Validar el formato del correo electrónico usando Regex
        Regex regex = new Regex(patron);
        return regex.IsMatch(email);
    }


    public Usuario ObtenerUsuarioPorEmail(string email)
    {
        return _usuarioRepository.ObtenerUsuarioPorEmail(email);
    }
}
