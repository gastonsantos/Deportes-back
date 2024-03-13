using Deportes.Servicio.Interfaces.IChathub;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Servicios.ChatServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Deportes.api.Controllers;

[ApiController]
[Route("chat")]
public class ChatController : Controller
{
    private readonly IHubContext<ChatHub> _hubContext;
    public IConfiguration _configuration;
    private readonly IChatHub _chatHub;


    public ChatController(IHubContext<ChatHub> hubContext, IConfiguration configuration, IChatHub chatHub)
    {
        _hubContext = hubContext;
        _configuration = configuration;
        _chatHub = chatHub;

    }
  

    [HttpPost]
    public async Task<IActionResult> EnviarMensaje(string user, string mensaje)
    {
        await _hubContext.Clients.All.SendAsync(user, mensaje);
       // await _chatHub.Send("1", user, mensaje);
            return Ok();
    }
}
