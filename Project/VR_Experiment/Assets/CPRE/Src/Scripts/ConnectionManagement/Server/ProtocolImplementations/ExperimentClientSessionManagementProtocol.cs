using System.Collections.Generic;
using CPRE.Scripts.ConnectionManagement.Server.Protocols;
using CPRE.Scripts.Experiment;
using CPRE.Scripts.Experiment.DataContainers;
using UnityEngine;

namespace CPRE.Scripts.ConnectionManagement.Server.ProtocolImplementations {
    
    [CreateAssetMenu(fileName = "SCB_ClientConnectionsHandler", menuName = "Networking/Experiment/ClientConnectionsHandler")]
    public class ExperimentClientSessionManagementProtocol : ScriptableObject, IClientSessionManagementProtocol {
        [SerializeField] private ParticipantRuntimeSet activeParticipants;
        [SerializeField] private GameObject participantPrefab;
        
        private Dictionary<ulong, ushort> _clientIdToParticipantIdMap;
        
        public void SetClientIdToParticipantIdMap(Dictionary<ulong, ushort> map) {
            _clientIdToParticipantIdMap = map;
        }

        public void HandleClientConnection(ulong clientId) {
            Debug.Log("Client Connected");
            
            if (_clientIdToParticipantIdMap.TryGetValue(clientId, out ushort participantId)) {
                var participantObject = GameObject.Instantiate(participantPrefab, Vector3.zero, Quaternion.identity);
                participantObject.GetComponent<Participant>().AssignParticipantID(participantId);
                participantObject.GetComponent<Participant>().networkClientId = clientId;
                activeParticipants.Add(participantObject.GetComponent<Participant>());
            }
        }
        
        public void HandleClientDisconnection(ulong clientId) {
            Debug.Log("Client Disconnected");

            if (_clientIdToParticipantIdMap.ContainsKey(clientId)) {
                ushort participantId = _clientIdToParticipantIdMap[clientId];
                _clientIdToParticipantIdMap.Remove(clientId);

                // Optionally, also remove the participant from _activeParticipants based on participantId
                foreach (var participant in activeParticipants.Items) {
                    if (participant.participantID.Value == participantId) {
                        activeParticipants.Remove(participant);
                        GameObject.Destroy(participant.gameObject);
                        break;
                    }
                }
            }
        }
    }
}