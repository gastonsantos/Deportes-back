using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Deportes.Modelo.CorreoModel;
using Deportes.Servicio.Interfaces.ICorreo;

namespace Deportes.Servicio.Servicios.CorreoServices;

public  class CorreoServices : ICorreoServices
{

    private static string _Host = "smtp.gmail.com";
    private static int _Puerto = 587;

    private static string _NombreEnvia = "Deportes, Tu Deporte";
    private static string _Correo = "deportestudeporte@gmail.com";
    private static string _Clave = "aouezxboczitdcza";
    // aoue zxbo czit dcza
    public bool Enviar(Correo correodto)
    {
        try
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
            email.To.Add(MailboxAddress.Parse(correodto.Para));
            email.Subject = correodto.Asunto;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = correodto.Contenido
            };

            var smtp = new SmtpClient();
            smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);

            smtp.Authenticate(_Correo, _Clave);
            smtp.Send(email);
            smtp.Disconnect(true);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
