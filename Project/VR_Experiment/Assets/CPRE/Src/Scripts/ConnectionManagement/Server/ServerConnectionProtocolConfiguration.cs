using System;
using CPRE.Scripts.ConnectionManagement.Server.Protocols;
using UnityEngine;

namespace CPRE.Scripts.ConnectionManagement.Server {
    [CreateAssetMenu(menuName = "Networking/Server Connection Configuration")]
    public class ServerConnectionProtocolConfiguration : ScriptableObject {
        [SerializeField] private ScriptableObject connectionApprovalManager;
        [SerializeField] private ScriptableObject clientConnectionsHandler;
        [SerializeField] private ScriptableObject networkObjectManager;
        
        public ScriptableObject ConnectionApprovalManagerSO => connectionApprovalManager;
        public ScriptableObject ClientConnectionsHandlerSO => clientConnectionsHandler;
        public ScriptableObject NetworkObjectManagerSO => networkObjectManager;
        
        public IAccessVerificationProtocol AccessVerificationProtocol => connectionApprovalManager as IAccessVerificationProtocol;
        public IClientSessionManagementProtocol ClientSessionManagementProtocol => clientConnectionsHandler as IClientSessionManagementProtocol;
        public INetworkAssetManagementProtocol NetworkAssetManagementProtocol => networkObjectManager as INetworkAssetManagementProtocol;

        public void Awake() {
            Validate();
        }

        public bool IsValid { get; private set; }

        public void Validate() {
            IsValid = 
                (connectionApprovalManager as IAccessVerificationProtocol) != null &&
                (clientConnectionsHandler as IClientSessionManagementProtocol) != null &&
                (networkObjectManager as INetworkAssetManagementProtocol) != null;
        }
    }
}