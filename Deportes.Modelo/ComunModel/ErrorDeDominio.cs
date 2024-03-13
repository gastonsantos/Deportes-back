using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.ComunModel;

public abstract class ErrorDeDominioException : Exception
{
    public CategoriaError Categoria { get;  }

    public string Mensaje { get; }

    protected ErrorDeDominioException(CategoriaError categoria, string mensaje) : base(mensaje)
    {
        Categoria = categoria;
        Mensaje = mensaje;
    }
}


public class ErrorGenericoException : ErrorDeDominioException
{
    private const string mensaje = "Un error ocurrió procesando tu solicitud";
    private const CategoriaError status = CategoriaError.SinEspecificar;

    public ErrorGenericoException() : base(status, mensaje) { }
}
