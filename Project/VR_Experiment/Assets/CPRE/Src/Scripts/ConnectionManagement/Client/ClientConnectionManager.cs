using CPRE.SOFramework.DataContainers.Variables;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Serialization;

namespace CPRE.Scripts.ConnectionManagement.Client {
    public class ClientConnectionManager : MonoBehaviour {
        
        [SerializeField] private StringReference ipAddress;
        [SerializeField] private IntReference port;
        [SerializeField] private IntReference requestedParticipantId;
        [SerializeField] private IntReference participantId;
        
        [SerializeField] private StringReference connectionInfoOutput;

        public void AttemptConnectionToServer() {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ipAddress, (ushort)port);
            NetworkManager.Singleton.NetworkConfig.ConnectionData =
                System.Text.Encoding.ASCII.GetBytes(requestedParticipantId.Value.ToString());
            NetworkManager.Singleton.StartClient();
        }
        
        private void Start() {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }
        
        private void OnClientConnectedCallback(ulong clientId) {
            Debug.Log("Successful Connection");
            connectionInfoOutput.Value = "Successfully connected to server, awaiting further instructions";
            participantId.Value = requestedParticipantId.Value;
        }
        
        private void OnClientDisconnectCallback(ulong clientId) {
            if(!NetworkManager.Singleton.IsServer) {
                Debug.Log("Client Disconnected");
                if(NetworkManager.Singleton.DisconnectReason != string.Empty) {
                    connectionInfoOutput.Value = $"Server Connection Request Denied, {NetworkManager.Singleton.DisconnectReason}";
                    Debug.Log(connectionInfoOutput.Value);
                }
            }
        }
    }
}