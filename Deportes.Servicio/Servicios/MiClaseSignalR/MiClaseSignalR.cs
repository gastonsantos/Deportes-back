using Deportes.Servicio.Interfaces.IParticipantes;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.MiClaseSignalR;

public class MiClaseSignalR : Hub
{
    public EscuchandoCambiosQuery sbAlertas = new EscuchandoCambiosQuery();


    public async Task EnviarMensaje(string user, string message)
    {
        await Clients.Caller.SendAsync("RespuestaDeSignalR", user, message);
    }


    public async Task IniciarEscuchaAlertas(int idUsuario)
    {
        // Agregar/Seleccionar cliente a nuestra clase a utilizar
        sbAlertas = EscuchandoCambiosQuery.cliente.GetOrAdd(Context.ConnectionId, sbAlertas);
        sbAlertas.callerContext = Context;
        sbAlertas.hubCallerClients = Clients;

        // Utilizar la clase
        sbAlertas.SetData(
          @"Data Source=DESKTOP-TS9IBN4;Initial Catalog=Deportes;Integrated Security=True; TrustServerCertificate=True;",
           $"SELECT IdParticipantes FROM dbo.Participante WHERE IdUsuarioParticipante={idUsuario}",
           "ALERTAS_ESCUCHA");
        sbAlertas.ReciboIdUsuario(idUsuario);
        sbAlertas.OnMensajeRecibido += new EscuchandoCambiosQuery.MensajeRecibido(sbAlertas_InformacionRecibida);
        sbAlertas.IniciarEscucha();
        await Clients.Caller.SendAsync("escuchaDeAlertasIniciada");
    }


    public async Task DetenerEscuchaAlertas(int idUsuario)
    {
        // Seleccionar cliente que va a detener la escucha
        sbAlertas = EscuchandoCambiosQuery.cliente.GetOrAdd(Context.ConnectionId, sbAlertas);

        // Detener la escucha
        sbAlertas.OnMensajeRecibido -= sbAlertas_InformacionRecibida;
        await Clients.Caller.SendAsync("Escucha de alertas detenida");
    }


    private static void sbAlertas_InformacionRecibida(object sender, string mensaje)
    {
        var sb = (EscuchandoCambiosQuery)sender;
        HubCallerContext hcallerContext = sb.callerContext;
        IHubCallerClients<IClientProxy> hubClients = sb.hubCallerClients;

        hubClients.Caller.SendAsync("AlertaDeNotificacion", mensaje);
    }





    public async Task Send(string cuentoId, string user, string message)
    {
        await Clients.All.SendAsync("Receive", cuentoId, user, message);
    }

    public async Task AddToGroup(string cuentoId, string nombre)
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, cuentoId);
        // Actualiza el registro de grupos activos
        await Clients.Group(cuentoId).SendAsync("ShowHoo", $"{nombre}, se UNIO al Chat");


    }

    public async Task LeaveGroup(string cuentoId, string nombre)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, cuentoId);
        await Clients.Group(cuentoId).SendAsync("ShowHooExit", $"{nombre}, SALIO del Chat");
    }

    public async Task SendToGroup(string cuentoId, string user, string message)
    {
        try
        {

            await Clients.Group(cuentoId).SendAsync("ReceiveMessage", cuentoId, user, message);

        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error al enviar notificación: {ex.Message}");
        }

    }
    public async Task SendToGroup1(string cuentoId, string user, string message, string targetUserId)
    {
        try
        {

            await Clients.Client(targetUserId).SendAsync("ReceiveMessage", cuentoId, user, message);
        }
        catch (Exception ex)
        {
            // Manejar la excepción (puedes imprimir en la consola, loggear, etc.)
            Console.WriteLine($"Error al enviar notificación: {ex.Message}");
        }
    }
}