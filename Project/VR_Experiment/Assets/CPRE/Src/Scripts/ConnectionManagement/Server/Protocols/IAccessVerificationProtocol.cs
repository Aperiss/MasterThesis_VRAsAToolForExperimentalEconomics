using System.Collections.Generic;
using Unity.Netcode;

namespace CPRE.Scripts.ConnectionManagement.Server.Protocols {
    public interface IAccessVerificationProtocol {
        void SetClientIdToParticipantIdMap(Dictionary<ulong, ushort> map);
        void CheckApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response);
    }
}