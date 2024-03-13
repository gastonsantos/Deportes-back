using Deportes.Servicio.Interfaces.IChathub;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Servicios.ParticipantesServices;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.ChatServices;

public class ChatHub : Hub, IChatHub
{
    private readonly  IParticipantesServices _participantesServices;

    public ChatHub(IParticipantesServices participantesServices)
    {
        _participantesServices = participantesServices;
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
            string connectionId = Context.ConnectionId;
            await Clients.Group(cuentoId).SendAsync("ReceiveMessage", cuentoId, user, connectionId);

          

            //_participantesServices.EnviarNotificacionParticipante(eventoId2, userId);
        }
        catch (Exception ex)
        {
            // Manejar la excepción (puedes imprimir en la consola, loggear, etc.)
            Console.WriteLine($"Error al enviar notificación: {ex.Message}");
        }

    }
    public async Task SendToGroup1(string cuentoId, string user, string message, string targetUserId)
    {
        try
        {
            string connectionId = Context.ConnectionId;
            // Enviar el mensaje solo al usuario específico
            await Clients.Client(targetUserId).SendAsync("ReceiveMessage", cuentoId, user, connectionId);

            int eventoId2 = 1;
            int userId = 1003;

            // Enviar notificación al usuario específico
            //_participantesServices.EnviarNotificacionParticipante(eventoId2, userId);
        }
        catch (Exception ex)
        {
            // Manejar la excepción (puedes imprimir en la consola, loggear, etc.)
            Console.WriteLine($"Error al enviar notificación: {ex.Message}");
        }
    }






}
