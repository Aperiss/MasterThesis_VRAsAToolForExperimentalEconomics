using System;
using System.Collections;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using Unity.Netcode;
using UnityEngine;

namespace CPRE.Scripts.Experiment {
    public class SessionManager : NetworkBehaviour {
        [Header("Network Variables")] 
        public NetworkVariable<ushort> treatmentId;
        public NetworkVariable<int> currentRound;
        
        [Header("Variable References")] 
        [SerializeField] private IntReference treatmentIdReference;
        [SerializeField] private IntReference currentRoundReference;
        [SerializeField] private BoolReference voteResultReference;
        
        [Header("Client Events")] 
        [SerializeField] private VoidEventChannel startExperimentEventChannel;
        [SerializeField] private VoidEventChannel endExperimentEventChannel;
        [SerializeField] private VoidEventChannel allParticipantsVotedEventChannel;
        [SerializeField] private VoidEventChannel continueExtractionsEventChannel;
        [SerializeField] private VoidEventChannel dontContinueExtractionsEventChannel;
        [SerializeField] private VoidEventChannel endManufacturingPhaseEventChannel;
        [SerializeField] private VoidEventChannel endRoundEventChannel;
        
        [Header("Server Events")]
        [SerializeField] private IntEventChannel notifyNextRoundEventChannel;
        
        private ushort _groupId;
        public ushort GroupId => _groupId;
        
        public void Initialize(ushort groupId, VoidEventChannel startExperimentEventChannel, VoidEventChannel endExperimentEventChannel) {
            this._groupId = groupId;
            this.currentRound.Value = 1;
            this.startExperimentEventChannel = startExperimentEventChannel;
            this.endExperimentEventChannel = endExperimentEventChannel;
        }
        
        private void Update() {
            treatmentIdReference.Value = treatmentId.Value;
            currentRoundReference.Value = currentRound.Value;
        }

        public void DeleteSessionManager() {
            Destroy(gameObject);
        }
        
        
        public void AssignTreatment(ushort groupTreatmentId) {
            treatmentId.Value = groupTreatmentId;
            treatmentIdReference.Value = groupTreatmentId;
        }
        
        public void StartExperiment() {
            StartExperimentClientRpc();
        }
        
        public void EndExperiment() {
            EndExperimentClientRpc();
        }
        
        public void BeginVoteResolution(bool result) {
            if (!IsOwner) {
                Debug.Log("BeginVoteResolution - Not the owner, returning.");
                return;
            }
            Debug.Log("BeginVoteResolution - Initiating vote resolution with result: " + result);
            StartCoroutine(VoteResolution(result, 5f));
        }

        private IEnumerator VoteResolution(bool result, float delay) {
            yield return new WaitForSeconds(delay);
            AllParticipantsVotedClientRpc();
            Debug.Log("All participants voted.");
            yield return new WaitForSeconds(delay);
            if (result) {
                Debug.Log("VoteResolution - Continuing extractions.");
                ContinueExtractionsClientRpc();
            } else {
                Debug.Log("VoteResolution - Not continuing extractions.");
                DontContinueExtractionsClientRpc();
            }
        }
        
        public void EndManufacturingPhase() {
            if (!IsOwner) {
                Debug.Log("EndManufacturingPhase - Not the owner, returning.");
                return;
            }
            Debug.Log("EndManufacturingPhase - Initiating end manufacturing phase.");
            StartCoroutine(EndManufacturingPhase(5f));
        }
        
        private IEnumerator EndManufacturingPhase(float delay) {
            yield return new WaitForSeconds(delay);
            EndManufacturingPhaseClientRpc();
            yield return new WaitForSeconds(10f);
            EndRoundClientRpc();
            notifyNextRoundEventChannel.Raise(_groupId);
        }

        public void StartNewRound() {
            currentRound.Value++;
        }
        
        [ClientRpc]
        private void EndManufacturingPhaseClientRpc() {
            Debug.Log("EndManufacturingPhaseClientRpc - Raising endManufacturingPhaseEventChannel event.");
            endManufacturingPhaseEventChannel.Raise();
        }

        [ClientRpc]
        private void AllParticipantsVotedClientRpc() {
            Debug.Log("AllParticipantsVotedClientRpc - Raising allParticipantsVotedEventChannel event.");
            allParticipantsVotedEventChannel.Raise();
        }

        [ClientRpc]
        private void DontContinueExtractionsClientRpc() {
            Debug.Log("DontContinueExtractionsClientRpc - Raising dontContinueExtractionsEventChannel event.");
            voteResultReference.Value = false;
            Debug.Log(voteResultReference.Value);
            dontContinueExtractionsEventChannel.Raise();
        }

        [ClientRpc]
        private void ContinueExtractionsClientRpc() {
            Debug.Log("ContinueExtractionsClientRpc - Raising continueExtractionsEventChannel event.");
            voteResultReference.Value = true;
            Debug.Log(voteResultReference.Value);
            continueExtractionsEventChannel.Raise();
        }
        
        [ClientRpc]
        private void EndRoundClientRpc() {
            endRoundEventChannel.Raise();
        }
        
        [ClientRpc]
        private void StartExperimentClientRpc() {
            startExperimentEventChannel.Raise();
        }

        [ClientRpc]
        private void EndExperimentClientRpc() {
            endExperimentEventChannel.Raise();
        }
    }
}