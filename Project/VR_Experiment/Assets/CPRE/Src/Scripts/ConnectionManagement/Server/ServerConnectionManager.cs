using System.Collections.Generic;
using CPRE.Scripts.ConnectionManagement.Server.Protocols;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace CPRE.Scripts.ConnectionManagement.Server {
    public class ServerConnectionManager : MonoBehaviour {

        [SerializeField] ServerConnectionProtocolConfiguration serverConnectionProtocolConfiguration;
        
        private Dictionary<ulong, ushort> _clientIdToParticipantIdMap;

        private IAccessVerificationProtocol AccessVerificationProtocol { get; set; }
        private IClientSessionManagementProtocol ClientSessionManagementProtocol { get; set; }
        private INetworkAssetManagementProtocol NetworkAssetManagementProtocol { get; set; }

        public void Awake() {
            _clientIdToParticipantIdMap = new Dictionary<ulong, ushort>();
            if(!InitializeServerConnectionConfiguration()) return;
            if (!SetClientParticipantMap()) return;
            SetNetworkManagerCallbacks();
        }

        private bool InitializeServerConnectionConfiguration() {
            if (serverConnectionProtocolConfiguration == null) {
                Debug.LogError("The server connection configuration is not set. Please ensure it is properly configured in the inspector.");
                return false;
            }
            
            if (!serverConnectionProtocolConfiguration.IsValid) {
                Debug.LogError("The server connection configuration is not valid. Please check the server configuration object to ensure they meet the necessary requirements.");
                return false;
            }
            
            AccessVerificationProtocol = serverConnectionProtocolConfiguration.AccessVerificationProtocol;
            ClientSessionManagementProtocol = serverConnectionProtocolConfiguration.ClientSessionManagementProtocol;
            NetworkAssetManagementProtocol = serverConnectionProtocolConfiguration.NetworkAssetManagementProtocol;
            
            if (AccessVerificationProtocol == null || ClientSessionManagementProtocol == null || NetworkAssetManagementProtocol == null) {
                Debug.LogError("The server dependencies are not referenced correctly. Please ensure that the ConnectionApprovalManager, ClientConnectionsHandler, and NetworkObjectManager are properly set up in the ServerConnectionConfiguration scriptable object.");
                return false;
            }

            return true;
        }
        private bool SetClientParticipantMap() {
            AccessVerificationProtocol.SetClientIdToParticipantIdMap(_clientIdToParticipantIdMap);
            ClientSessionManagementProtocol.SetClientIdToParticipantIdMap(_clientIdToParticipantIdMap);
            return true;
        }

        private void SetNetworkManagerCallbacks() {
            NetworkManager.Singleton.OnServerStarted += OnServerStarted;
            NetworkManager.Singleton.OnClientConnectedCallback += ClientSessionManagementProtocol.HandleClientConnection;
            NetworkManager.Singleton.OnClientDisconnectCallback += ClientSessionManagementProtocol.HandleClientDisconnection;
            NetworkManager.Singleton.ConnectionApprovalCallback = AccessVerificationProtocol.CheckApproval;
            
            Debug.Log("Network Manager callbacks set.");
        }
        
        public void StartServer() {
            NetworkManager.Singleton.StartServer();
        }
        
        private void OnServerStarted() {
            Debug.Log("Server Started");
        }

        public void OnGroupsReady() {
            NetworkAssetManagementProtocol.SpawnNetworkObjects();
        }

        public void OnEndExperiment() {
            NetworkAssetManagementProtocol.DestroyNetworkObjects();
        }
    }
}