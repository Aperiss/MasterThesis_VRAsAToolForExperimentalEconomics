using System;
using System.Collections;
using CPRE.Scripts.Experiment.DataContainers;
using CPRE.Scripts.Experiment.Events;
using CPRE.Scripts.Experiment.Events.Extractions;
using CPRE.Scripts.Experiment.Events.Votes;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace CPRE.Scripts.Experiment {
    public class Participant : NetworkBehaviour {

        public ParticipantRuntimeSet participantRuntimeSet; 
        public ulong networkClientId;
        
        [Header("Network Variables")]
        public NetworkVariable<ushort> participantID = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<ushort> groupID = new NetworkVariable<ushort>();
        public NetworkVariable<int> tokens = new NetworkVariable<int>();
        public NetworkVariable<int> logs = new NetworkVariable<int>();
        
        [Header("Variable References")]
        [SerializeField] private IntReference participantIdReference;
        [SerializeField] private IntReference logsExtractedReference;
        [SerializeField] private IntReference participantExtractionsThisRoundReference;
        [SerializeField] private IntReference tokensReference;

        [Header("ClientEvents")]
        [SerializeField] private IntEventChannel logAddedToStockpileEventChannel;
        
        [Header("Server Events")] 
        [SerializeField] private ExtractionRequestEventChannel resourceExtractionRequestEventChannel;
        [SerializeField] private ExtractionVoteRequestEventChannel resourceExtractionVoteRequestEventChannel;
        [SerializeField] private IntEventChannel participantFinishedManufacturingEventChannel;
        
        public void Update() {
            participantIdReference.Value = participantID.Value;
            logsExtractedReference.Value = logs.Value;
            tokensReference.Value = tokens.Value;
        }

        public void AssignParticipantID(ushort participantId) {
            participantID.Value = participantId;
            participantIdReference.Value = participantId;
        }
        
        public void AssignGroupID(ushort id) {
            groupID.Value = id;
        }

        public void RequestNextRound() {
            if (!IsOwner) return;
            RequestNextRoundServerRpc();
        }
        
        [ServerRpc]
        private void RequestNextRoundServerRpc() {
            Debug.Log($"Participant {participantID.Value} requested new round.");
        }
        
        public void RequestExtraction(int amount) {
            if (!IsOwner) return;
            RequestExtractionServerRpc(amount);
        }
        
        [ServerRpc]
        private void RequestExtractionServerRpc(int amount) {
            Debug.Log("ServerRpc called.");
            ExtractionRequestData requestData = new ExtractionRequestData();
            requestData.participantId = participantID.Value;
            requestData.extractionAmount = amount;
    
            resourceExtractionRequestEventChannel.Raise(requestData);
        }

        public void ProcessExtractionResponse(ExtractionResponseData responseData) {
            if (!responseData.isApproved) {
                Debug.Log("Extraction denied.");
                NotifyExtractionFailedClientRpc();
                return;
            }
            Debug.Log($"Extraction approved, extracting...{responseData.extractionAmount}");
            logs.Value += responseData.extractionAmount;
            NotifyExtractionResponseClientRpc(responseData.extractionAmount);
        }
        
        [ClientRpc]
        private void NotifyExtractionFailedClientRpc() {
            Debug.Log("Extraction failed.");
        }
        
        [ClientRpc]
        private void NotifyExtractionResponseClientRpc(int amount) {
            Debug.Log($"Extraction approved, extracting...{amount}");
            participantExtractionsThisRoundReference.Value += amount;
            StartCoroutine(AddLogsToStockpile(amount, 1f));
        }
        
        private IEnumerator AddLogsToStockpile(int amount, float totalSeconds) {
            float timePerEmit = totalSeconds / amount;
    
            for (int i = 0; i < amount; i++) {
                logAddedToStockpileEventChannel.Raise(1);
                Debug.Log("Log added to stockpile");
                yield return new WaitForSeconds(timePerEmit);
            }
    
            Debug.Log($"All logs added to stockpile.");
        }
        
        public void VoteForExtraction(bool vote) {
            if (!IsOwner) return;
            Debug.Log("Cast vote");
            VoteForExtractionServerRpc(vote);
        }
        
        [ServerRpc]
        private void VoteForExtractionServerRpc(bool vote) {
            ExtractionVoteRequestData requestData = new ExtractionVoteRequestData();
            requestData.participantId = participantID.Value;
            requestData.vote = vote;
    
            resourceExtractionVoteRequestEventChannel.Raise(requestData);
        }

        public void OnLogManufactured() {
            if (!IsOwner) return;
            OnLogManufacturedServerRpc();
        }
        
        [ServerRpc]
        public void OnLogManufacturedServerRpc() {
            logs.Value--;
            tokens.Value++;
        }
        
        public void OnAllLogsManufactured() {
            if (!IsOwner) return;
            participantExtractionsThisRoundReference.Value = 0;
            OnAllLogsManufacturedServerRpc();
        }
        
        [ServerRpc]
        public void OnAllLogsManufacturedServerRpc() {
            participantFinishedManufacturingEventChannel.Raise(participantID.Value);
        }
    }
}