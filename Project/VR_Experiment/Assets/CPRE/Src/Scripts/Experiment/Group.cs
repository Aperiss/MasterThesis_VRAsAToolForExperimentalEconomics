using System.Collections.Generic;
using CPRE.Scripts.Experiment.Events;
using System.Linq;
using CPRE.Scripts.Experiment.Events.Extractions;
using UnityEngine;

namespace CPRE.Scripts.Experiment {
    [System.Serializable]
    public class Group {
        [SerializeField] 
        private ushort groupId;
        public ushort GroupId => groupId;

        [SerializeField] 
        private ushort treatmentId;
        public ushort TreatmentId => treatmentId;

        [SerializeField] 
        private int groupSize;
        public int GroupSize => groupSize;

        [SerializeField] private CommonPool commonPool;
        [SerializeField] private SessionManager sessionManager;
        [SerializeField] private Dictionary<ushort, Participant> participants = new Dictionary<ushort, Participant>();

        private Dictionary<ushort, int> _participantExtractionRequests = new Dictionary<ushort, int>();
        private Dictionary<ushort, bool> _participantExtractionVotes = new Dictionary<ushort, bool>();
        private Dictionary<ushort, bool> _participantFinishedManufacturing = new Dictionary<ushort, bool>();

        public Group(ushort id) {
            groupId = id;
        }
        
        public void AssignCommonPool(CommonPool pool) {
            commonPool = pool;
            Debug.Log("Common pool assigned to group " + groupId);
        }
        public CommonPool GetCommonPool() {
            return commonPool;
        }
        
        public void AssignSessionManager(SessionManager manager) {
            sessionManager = manager;
        }
        public SessionManager GetSessionManager() {
            return sessionManager;
        }
        
        public void AddParticipant(ushort participantId, Participant participantReference) {
            if(participants.TryAdd(participantId, participantReference)) {
                groupSize++;
                commonPool.SetTotalParticipants(groupSize);
            }
        }

        public void RemoveParticipant(ushort participantId) {
            if(participants.Remove(participantId)) {
                groupSize--;
                commonPool.SetTotalParticipants(groupSize);
            }
        }

        public bool ContainsParticipant(ushort participantId) {
            return participants.ContainsKey(participantId);
        }

        public IEnumerable<Participant> GetParticipantsInGroup() {
            return participants.Values;
        }
        
        public void AssignTreatmentType(ushort id) {
            treatmentId = id;
            sessionManager.treatmentId.Value = id;
        }
        
        public void OnParticipantRequestedExtraction(ushort participantId, int amount) {
            if (!participants.ContainsKey(participantId)) {
                Debug.Log($"Participant {participantId} not found in group {groupId}.");
                return;
            }

            _participantExtractionRequests[participantId] = amount;
            Debug.Log($"Extraction request received from participant {participantId} for amount {amount} in group {groupId}.");

            if (_participantExtractionRequests.Count == groupSize) {
                Debug.Log($"All extraction requests received for group {groupId}. Processing Extractions.");
                ProcessExtractions();
            }
        }
        
        public bool ProcessExtractions() {
            int extractionAmount = 0;
    
            foreach (var extractionRequest in _participantExtractionRequests.Values) {
                extractionAmount += extractionRequest;
            }

            Debug.Log($"Total extraction amount requested in group {groupId}: {extractionAmount}. Processing...");
            bool result = commonPool.BeginResourceExtraction(extractionAmount);
    
            if(result) Debug.Log($"Extraction approved for group {groupId}.");
            else Debug.Log($"Extraction denied for group {groupId}.");
            
            return result; 
        }
        
        public void NotifyParticipantsOfIndividualExtractions() {
            foreach (var extractionRequest in _participantExtractionRequests) {
                if (participants.TryGetValue(extractionRequest.Key, out var participant)) {
                    var extractionResponseData = new ExtractionResponseData {
                        participantId = extractionRequest.Key,
                        extractionAmount = extractionRequest.Value,
                        isApproved = true // Or any condition you want to check before approving
                    };
            
                    participant.ProcessExtractionResponse(extractionResponseData);
                    Debug.Log($"Extraction response processed for participant {extractionRequest.Key} with amount {extractionResponseData.extractionAmount} in group {groupId}.");
                }
            }
    
            _participantExtractionRequests.Clear();
            Debug.Log($"Extraction requests cleared for group {groupId}.");
        }

        public void OnParticipantVotedForExtraction(ushort participantId, bool vote){
            if (!participants.ContainsKey(participantId)) {
                Debug.Log($"Participant {participantId} not found in group {groupId}.");
                return;
            }

            _participantExtractionVotes[participantId] = vote;
            Debug.Log($"Extraction vote received from participant {participantId} for vote {vote} in group {groupId}.");

            if (_participantExtractionVotes.Count == groupSize) {
                Debug.Log($"All extraction votes received for group {groupId}. Processing Votes.");
                ProcessVotes();
            }
        }
        
        public bool ProcessVotes() {
            int trueVotesCount = _participantExtractionVotes.Values.Count(v => v);
            bool result = trueVotesCount > groupSize / 2;
    
            Debug.Log($"Votes processed for group {groupId}. Majority voted: {result}");
            
            sessionManager.BeginVoteResolution(result);
            
            _participantExtractionVotes.Clear(); // Clearing the votes after processing
            return result; 
        }
        
        public void OnParticipantFinishedManufacturing(ushort participantId) {
            if (!participants.ContainsKey(participantId)) {
                Debug.Log($"Participant {participantId} not found in group {groupId}.");
                return;
            }

            _participantFinishedManufacturing[participantId] = true;
            Debug.Log($"Manufacturing finished received from participant {participantId} in group {groupId}.");

            if (_participantFinishedManufacturing.Count == groupSize) {
                Debug.Log($"All manufacturing finished received for group {groupId}. Processing...");
                ProcessManufacturing();
            }
        }
        
        public void ProcessManufacturing() {
            Debug.Log($"Manufacturing processed for group {groupId}.");
            sessionManager.EndManufacturingPhase();
            _participantFinishedManufacturing.Clear(); // Clearing the votes after processing
        }
        
        public void StartNewRound() {
            Debug.Log($"Beginning regeneration for group {groupId}.");
            commonPool.BeginRegeneration();
            sessionManager.StartNewRound();
        }
        
    }
}
