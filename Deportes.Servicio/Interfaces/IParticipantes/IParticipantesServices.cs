﻿using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.ParticipanteModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IParticipantes
{
    public interface IParticipantesServices
    {
        public IList<Participante> ObtengoNotificacionParticipante(int idUsuario);
        public void EnviarNotificacionParticipante(int idEvento, int idUsuarioQueInvita, int idUsuarioInvitado);

        public void EliminarParticipante(int idParticipante);

        public void AceptarParticipante(int idParticipantet);
        public List<DtoNotificacion> ObtenerNotificacionesPorUsuario(int idUsuario);
        public Participante ObtenerParticipantePorIdParticipante(int idParticipante);
        public bool PuedeAceptarNotificacion(int idParticipante);
        public bool YaHayNotificacion(int idEvento, int idUsusarioQueInvita, int idUsuarioInvitado);
    }
}
