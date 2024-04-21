using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deportes.Modelo.UsuarioModel;

namespace Deportes.Servicio.Servicios.MiClaseSignalR
{
    public class ServiceBroker
    {
        public class ServiceBrokerSQL
        {
            private string nombreMensaje = "";
            private string cadenaConexion = "";
            private string comandoEscucha = "";
            private SqlConnection conexion;

            public delegate void MensajeRecibido(object sender, string nombreMensaje);
            public event MensajeRecibido? OnMensajeRecibido = null;

    
            public ServiceBrokerSQL(string connectionString, string SQLquery, string nombreMensaje)
            {
                if (connectionString == "" || SQLquery == "") throw new ApplicationException("Debe indicar la cadena de conexión y el comando de escucha");
                this.cadenaConexion = connectionString;
                this.comandoEscucha = SQLquery;
                this.nombreMensaje = nombreMensaje;
                // Iniciar la escucha. El usuario debe tener permisos de SUBSCRIBE QUERY NOTIFICATIONS. La base de datos debe tener SSB enabled. ALTER DATABASE NombreBaseDatos SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE.
                SqlDependency.Start(cadenaConexion);
                // Crear la conexión a base de datos.
                conexion = new SqlConnection(cadenaConexion);
            }

       
            ~ServiceBrokerSQL()
            {
                // Detener la escucha antes de salir.
                SqlDependency.Stop(cadenaConexion);
            }

            public void IniciarEscucha()
            {
                try
                {
                    // Crear el comando de escucha. Debe incluir el esquema y no usar *. Ejemplo: SELECT campo FROM dbo.Tabla
                    SqlCommand cmd = new SqlCommand(comandoEscucha, conexion);
                    cmd.CommandType = CommandType.Text;

                    // Limpiar cualquier notificación existente
                    cmd.Notification = null;

                    // Crear una dependencia para el comando
                    SqlDependency dependency = new SqlDependency(cmd);

                    // Añadir un controlador del evento
                    dependency.OnChange += new OnChangeEventHandler(OnChange);

                    // Abrir la conexión si es necesario
                    if (conexion.State == ConnectionState.Closed) conexion.Open();

                    // Obtener los mensajes y luego cerrar la conexión
                    cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public void DetenerEscucha()
            {
                SqlDependency.Stop(cadenaConexion);
            }

            private void OnChange(object sender, SqlNotificationEventArgs e)
            {
                SqlDependency dependency = (SqlDependency)sender;

                // Las notificaciones son un aviso de un sólo disparo, así que se debe eliminar el existente para poder agregar uno nuevo.
                dependency.OnChange -= OnChange;

                // Disparar el evento
                if (OnMensajeRecibido != null)
                {
                    OnMensajeRecibido(this, new string(nombreMensaje));
                }
            }
            public string ObtenerResultadoConsulta(int idUsuario)
            {
                try
                {
                    StringBuilder resultadoConsulta = new StringBuilder();

                    using (var connection = new SqlConnection(cadenaConexion))
                    {
                        connection.Open();
                        // Modificar la consulta aquí
                        string nuevaConsulta = $"SELECT * FROM dbo.Participante WHERE IdUsuarioParticipante={idUsuario}";
                        using (var command = new SqlCommand(nuevaConsulta, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        // Agregar el valor de cada columna al StringBuilder
                                        resultadoConsulta.Append(reader[i].ToString());
                                        if (i < reader.FieldCount - 1)
                                        {
                                            resultadoConsulta.Append(", ");
                                        }
                                    }
                                    resultadoConsulta.AppendLine(); // Nueva línea para cada fila
                                }
                            }
                        }
                    }
                    //return "LALALAL";
                    return resultadoConsulta.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener resultado de la consulta: {ex.Message}");
                    return string.Empty; // Manejar el error según tus necesidades
                }
            }


        }
    }
}
