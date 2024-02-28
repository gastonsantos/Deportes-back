using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.UsuarioRepository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DeportesContext _context;

    public UsuarioRepository(DeportesContext context)
    {
        _context = context;
    }

    public IList<Usuario> GetAll() //Todos los usuarios
    {
        return _context.Usuario.ToList();
    }

    public Usuario? ObtenerUsuarioMailContraseña(string email, string contra)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Contrasenia == contra);
    }

    public Usuario? ObtenerUsuarioPorId(int id)
    {
        return _context.Usuario.FirstOrDefault(u => u.Id == id);
    }



}
