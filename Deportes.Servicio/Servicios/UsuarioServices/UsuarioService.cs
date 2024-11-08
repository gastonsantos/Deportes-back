﻿using Deportes.Modelo.CorreoModel;
using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.ICorreo;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.FichaServices;
using Deportes.Servicio.Servicios.FichaServices.Dto;
using Deportes.Servicio.Servicios.UsuarioServices.Dto;
using Deportes.Servicio.Servicios.UsuarioServices.Errores;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.UsuarioServices;

public class UsuarioService : IUsuarioService
{
    private readonly ICorreoServices _correoServices;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IFichaRepository _fichaRepository;
    public UsuarioService (IUsuarioRepository usuarioRepository, ICorreoServices correoServices, IFichaRepository fichaRepository)
    {
        _usuarioRepository = usuarioRepository;
        _correoServices = correoServices;
        _fichaRepository = fichaRepository;
    }

    public IList<DtoUsuario> GetAll()
    {
        return _usuarioRepository.GetAll();         
    }

    public DtoUsuario GetUsuarioParaPerfilInvitacion(int id)
    {
        var usuario = _usuarioRepository.ObtenerUsuarioPorId(id);
        if (usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        DtoUsuario dtoUsuario = new DtoUsuario();
        dtoUsuario.Id = usuario.Id;
        dtoUsuario.Nombre = usuario.Nombre;
        dtoUsuario.Apellido= usuario.Apellido;
        dtoUsuario.Apodo  = usuario.Apodo;
        dtoUsuario.Email = usuario.Email;
        return dtoUsuario;
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
        
            return _usuarioRepository.ObtenerUsuarioPorId(id);
        
       
    }

    public void GuardarUsuarioEnBd(string nombre, string apellido,string apodo, string email, string contrasenia, string provincia, string localidad, string direccion, string numero)
    {
        Usuario usuario = new Usuario();
       
        usuario.Nombre = nombre;
        usuario.Apellido = apellido;
        usuario.Apodo = apodo;
        usuario.Email = email;
        usuario.Contrasenia = contrasenia;
        usuario.Provincia= provincia;
        usuario.Localidad= localidad;
        usuario.Direccion = direccion;
        usuario.Numero= numero;
        usuario.TokenConfirmacion = GenerarToken();
        usuario.Activo = true;
        usuario.VerifyEmail = false;
            
        if(usuario.Nombre == null || usuario.Email == null)
        {
             throw new  UsuarioEsNullException();
        }
        if(_usuarioRepository.ObtenerUsuarioPorEmail(email) != null)
        {
            throw new UsuarioYaExisteException();
        }

        if ( !EmailConForamtoValido(email))
        {
            throw new FormatoEmailInvalidoException();
        }

        var idUsuario =  _usuarioRepository.GuardarUsuarioEnBd(usuario);
        
        EnviarCorreoConfirmacion(usuario);

        AgregoFichaDeportistaBasica(idUsuario);

        AgregarFichaFutbolBasica(idUsuario);


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

    public Usuario ObtenerUsuarioPorToken(string token)
    {
        return _usuarioRepository.ObtenerUsuarioPorToken(token);
    }

    private static string GenerarToken()
    {
        // Reemplazar "claveSecreta" por una clave segura
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("claveSecreta"));
        var tokenBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        return Convert.ToBase64String(tokenBytes)
               .Replace('+', '-')
               .Replace('/', '_')
               .Replace("=", ""); 
    }

    private void EnviarCorreoConfirmacion(Usuario usuario)
    {
        var correo = new Correo
        {
            Para = usuario.Email,
            Asunto = "Confirma tu cuenta",
            Contenido = $"Hola {usuario.Nombre},<br>Para confirmar tu cuenta, haz clic en el siguiente enlace:<br><a href='https://deportes-front.vercel.app/pages/confirmarMail/{usuario.TokenConfirmacion}'>Confirmar cuenta</a>"
        };
        _correoServices.Enviar(correo);
       
    }

    public bool ConfirmarEmailUsuario(string token)
    {
        var usuario = ObtenerUsuarioPorToken(token);
        if (usuario == null) { 
        return false;
        }

        if (ConfirmarEmailUsuarioYNullearToken(token))
        {
            return true;
        }

        return true;
    }

    public bool ConfirmarEmailUsuarioYNullearToken(string token)
    {
        return _usuarioRepository.ConfirmarEmailUsuarioYNullearToken(token);
    }

    private void EnviarCorreoCambiarContrasenia(Usuario usuario)
    {
        var correo = new Correo
        {
            Para = usuario.Email,
            Asunto = "Cambiar Contraseña",
            Contenido = $"Hola {usuario.Nombre},<br>Si usted no pidio cambiar contraseña ignorar este mensaje,si no, haz clic en el siguiente enlace:<br><a href='https://deportes-front.vercel.app/pages/cambioContrasenia/{usuario.TokenCambioContrasenia}'>Cambiar</a>"
        };
        _correoServices.Enviar(correo);

    }

    
    public bool EnvioCambiarContrasenia(string email)
    {
        var usuario = ObtenerUsuarioPorEmail(email);
        if (usuario == null)
        {
            return false;
        }
        string token = GenerarToken();

        _usuarioRepository.GuardoTokenCambioContraseniaPorEmailUsuario(email, token);
   
        EnviarCorreoCambiarContrasenia(usuario);

        return true;
    }

    public bool CambioContrasenia(string constraseniaNueva, string token)
    {
        var usuario = _usuarioRepository.ObtenerUsuarioPorTokenCambioContrasenia(token);
        if(usuario == null)
        {
            return false;
        }

        _usuarioRepository.CambioContraseniaPorToken(constraseniaNueva, token);
       
        return true;
    }

        
    private void AgregoFichaDeportistaBasica(int idUsuario)
    {
        FichaDeportistum fichaDeportista = new FichaDeportistum();
        fichaDeportista.IdUsuario= idUsuario;
        fichaDeportista.Avatar = "";
        fichaDeportista.Altura = "0";
        fichaDeportista.Edad = "0";
        fichaDeportista.Peso="0";
        fichaDeportista.ManoHabil = "Derecha";
        fichaDeportista.PieHabil = "Derecho";
       

        _fichaRepository.AgregarFichaDeportista(fichaDeportista);
    }

    private void AgregarFichaFutbolBasica(int idUsuario)
    {
        FichaFutbol fichaFutbol = new FichaFutbol();
        fichaFutbol.IdUsuario= idUsuario;
        
        fichaFutbol.Velocidad = 0;
        fichaFutbol.Resistencia = 0;
        fichaFutbol.Precision = 0;
        fichaFutbol.Fuerza = 0;
        fichaFutbol.Tecnica = 0;
        fichaFutbol.Agilidad = 0;
        fichaFutbol.Media = 0;
       
       

        _fichaRepository.AgregarFichaFutbol(fichaFutbol);
    }


}
