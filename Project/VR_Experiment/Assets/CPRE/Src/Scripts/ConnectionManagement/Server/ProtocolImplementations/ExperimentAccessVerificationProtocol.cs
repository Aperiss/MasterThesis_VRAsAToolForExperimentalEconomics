using System;
using System.Collections.Generic;
using CPRE.Scripts.ConnectionManagement.Server.Protocols;
using Unity.Netcode;
using UnityEngine;

namespace CPRE.Scripts.ConnectionManagement.Server.ProtocolImplementations {
    [CreateAssetMenu(fileName = "SCP_AccessVerificationProtocol", menuName = "Networking/ServerConnectionProtocols/Experiment/AccessVerificationProtocol")]
    public class ExperimentAccessVerificationProtocol : ScriptableObject, IAccessVerificationProtocol {
        private Dictionary<ulong, ushort> _clientIdToParticipantIdMap;
        public bool verboseMessages = false; // Add this variable to control the verbosity of the logging

        public void SetClientIdToParticipantIdMap(Dictionary<ulong, ushort> map) {
            this._clientIdToParticipantIdMap = map;
            if(verboseMessages) {
                Debug.Log("ClientId to ParticipantId map has been set.");
            }
        }
        
        public void CheckApproval(NetworkManager.ConnectionApprovalRequest request,
            NetworkManager.ConnectionApprovalResponse response) {
            var clientId = request.ClientNetworkId;
            var connectionPayload = request.Payload;
            Debug.Log("Connection request received");

            if(verboseMessages) {
                Debug.Log("Checking approval for connection request.");
                Debug.Log("Client Network ID: " + clientId);
                Debug.Log("Payload: " + BitConverter.ToString(connectionPayload));
            }

            ushort participantId;
            try {
                participantId = ushort.Parse(System.Text.Encoding.ASCII.GetString(connectionPayload));
                Debug.Log("Participant ID: " + participantId);
                if(verboseMessages) {
                    Debug.Log("Parsed participant ID successfully.");
                }
            }
            catch (FormatException fe) {
                Debug.LogError("Invalid participant ID format: " + fe.Message);
                response.Approved = false;
                response.Reason = "Invalid participant ID format";
                return;
            }

            if (!_clientIdToParticipantIdMap.ContainsValue(participantId)) {
                Debug.Log(participantId + " is not in use, approving connection");
                if(verboseMessages) {
                    Debug.Log("Participant ID not found in the map, approving connection.");
                }
                _clientIdToParticipantIdMap.Add(clientId, participantId);
                response.Approved = true;
            }
            else {
                if(verboseMessages) {
                    Debug.Log("Participant ID found in the map, rejecting connection.");
                }
                response.Approved = false;
                response.Reason = "Participant ID already in use, please select a unique ID";
            }
            

            if(verboseMessages) {
                Debug.Log("Response: " + (response.Approved ? "Approved" : "Not Approved") + ", Reason: " + response.Reason);
            }
        }
    }
}