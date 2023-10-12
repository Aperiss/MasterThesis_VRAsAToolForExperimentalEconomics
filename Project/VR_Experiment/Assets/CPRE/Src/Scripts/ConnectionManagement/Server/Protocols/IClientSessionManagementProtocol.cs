using System.Collections.Generic;

namespace CPRE.Scripts.ConnectionManagement.Server.Protocols {
    public interface IClientSessionManagementProtocol {
        void SetClientIdToParticipantIdMap(Dictionary<ulong, ushort> map);
        void HandleClientConnection(ulong clientId);
        void HandleClientDisconnection(ulong clientId);
    }
}