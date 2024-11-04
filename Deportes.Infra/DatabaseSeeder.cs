using Deportes.Modelo.UsuarioModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra
{
    public static class DatabaseSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DeportesContext>();

                // Aplica las migraciones pendientes
                context.Database.Migrate();

                // Si ya hay datos, no se agrega más
                if (context.Usuario.Any())
                {
                    return; // Ya hay datos en la base de datos
                }

                // Aquí puedes agregar datos iniciales
                context.Usuario.AddRange(
                    new Usuario
                    {
                        Nombre = "Juan",
                        Apellido = "Pérez",
                        Apodo = "Juano",
                        Email = "juan.perez@example.com",
                        Contrasenia = "ContraseniaSegura123", // Cambia esto a un hash en producción
                        Provincia = "Buenos Aires",
                        Localidad = "CABA",
                        Direccion = "Av. Corrientes",
                        Numero = "1234",
                        VerifyEmail = true, // Puedes usar true en lugar de 1
                        Activo = true,      // Puedes usar true en lugar de 1
                        TokenConfirmacion = null,
                        TokenCambioContrasenia = null
                    }
                );

                context.SaveChanges();
            }
        }
    }


}


