using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Deportes.Servicio.Servicios.MiClaseSignalR.ServiceBroker;

namespace Deportes.Servicio.Servicios.MiClaseSignalR;

public class EscuchandoCambiosQuery
{

    // de forma que se pueda identificar al cliente que está haciendo la llamada
    public HubCallerContext? callerContext { get; set; }
    public IHubCallerClients<IClientProxy>? hubCallerClients { get; set; }

    public static ConcurrentDictionary<string, EscuchandoCambiosQuery> cliente = new ConcurrentDictionary<string, EscuchandoCambiosQuery>();

    // Variables miembro
    public string _cadenaConexion { get; set; }
    public string _query { get; set; }
    public string _nombreMensaje { get; set; }

    public delegate void MensajeRecibido(object sender, string nombreMensaje);
    public event MensajeRecibido? OnMensajeRecibido = null;

    // Clase principal que utilizaremos
    ServiceBrokerSQL sb;

    
    /// Necesitamos que no se inicialice de inicio, ya que será usado por el Hub en diferentes llamadas que establecerán los parámetros de la query
   
    public EscuchandoCambiosQuery()
    {
    }

    
    public void SetData(string connectionString, string SQLquery, string nombreMensaje)
    {
        _cadenaConexion = connectionString;
        _query = SQLquery;
        _nombreMensaje = nombreMensaje;
    }

    public void IniciarEscucha()
    {
        sb = new ServiceBrokerSQL(_cadenaConexion, _query, _nombreMensaje);
        sb.OnMensajeRecibido += new ServiceBrokerSQL.MensajeRecibido(sb_InformacionRecibida);
        sb.IniciarEscucha();
    }

    public void DetenerEscucha()
    {
        sb.DetenerEscucha();
    }


    // Evento de cambio
    private void sb_InformacionRecibida(object sender, string nombreMensaje)
    {
        if (OnMensajeRecibido != null)
        {
            OnMensajeRecibido.Invoke(this, new string("Se ah recibido una notificación"));
            this.IniciarEscucha();
        }
    }


}
