using System;
using System.Collections;
using CPRE.Scripts.Experiment.Events;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace CPRE.Scripts.Experiment {
    public class CommonPool : NetworkBehaviour {

        #region Fields and Properties
        [Header("Network Variables")] 
        private NetworkVariable<int> _currentResources = new NetworkVariable<int>();
        private NetworkVariable<int> _maxCapacity = new NetworkVariable<int>();
        private NetworkVariable<int> _allowedExtractionsPerParticipant = new NetworkVariable<int>();
        private NetworkVariable<int> _resourceRegenerationRate = new NetworkVariable<int>();

        [Header("Variable References")]
        [SerializeField] private IntReference currentResourcesReference;
        [SerializeField] private IntReference maxCapacityReference;
        [SerializeField] private IntReference allowedExtractionsPerParticipantReference;
        [SerializeField] private IntReference resourceRegenerationRateReference;
        [SerializeField] private IntReference extractionsThisRoundReference;

        [Header("Client Events")] 
        [SerializeField] private VoidEventChannel beginExtractionProcessEventChannel;
        [SerializeField] private IntEventChannel extractResourceEventChannel;
        [SerializeField] private VoidEventChannel endExtractionProcessEventChannel;
        [SerializeField] private VoidEventChannel beginRegenerationProcessEventChannel;
        [SerializeField] private IntEventChannel regenerateResourceEventChannel;
        [SerializeField] private VoidEventChannel endRegenerationProcessEventChannel;
        
        [Header("Server Events")]
        [SerializeField] private IntEventChannel extractionsCompletedEventChannel;
        
        private int _totalParticipants;
        private int _maxExtractionsPerRound;
        private int _remainingExtractionsThisRound;
        private int _extractionsThisRound;
        private int _groupId;

        public int GroupId => _groupId;
        #endregion

        #region Initialization
        public void Initialize(
            ushort poolId,
            int capacity,
            int initialResources,
            int extractionsPerParticipant,
            int regenerationRate) {
            _groupId = poolId;
            _maxCapacity.Value = capacity;
            _currentResources.Value = initialResources;
            _resourceRegenerationRate.Value = regenerationRate;
            _allowedExtractionsPerParticipant.Value = extractionsPerParticipant;
            Debug.Log("Pool Initialized");
        }

        public void SetTotalParticipants(int totalParticipants) {
            _totalParticipants = totalParticipants;
            UpdateExtractionLimits();
            Debug.Log($"Total Participants set to {_totalParticipants}");
        }
        #endregion
        
        #region Update Methods
        void Update() {
            if (!IsServer) UpdateReferences();
        }

        private void UpdateReferences() {
            currentResourcesReference.Value = _currentResources.Value;
            maxCapacityReference.Value = _maxCapacity.Value;
            allowedExtractionsPerParticipantReference.Value = _allowedExtractionsPerParticipant.Value;
            resourceRegenerationRateReference.Value = _resourceRegenerationRate.Value;
        }
        #endregion

        #region Regeneration Methods
        public void BeginRegeneration() {
            
            int regenerationAmount = _resourceRegenerationRate.Value;

             if (_currentResources.Value + regenerationAmount > _maxCapacity.Value) {
                 regenerationAmount = _maxCapacity.Value - _currentResources.Value;
             }
            StartCoroutine(DelayedRegeneration(regenerationAmount, 2f));
            Log($"Beginning Resource Regeneration: {_resourceRegenerationRate.Value} resources");
        }

        private IEnumerator DelayedRegeneration(int regenerationAmount, float approximateDuration) {
            yield return new WaitForSeconds(2f);
            
            NotifyBeginRegenerationProcessClientRpc(regenerationAmount);
            yield return StartCoroutine(ProgressiveRegeneration(regenerationAmount, approximateDuration));
            
            yield return new WaitForSeconds(2f);
            NotifyEndRegenerationProcessClientRpc();
            Log($"Resources Regenerated: {regenerationAmount}");
        }
        
        private IEnumerator ProgressiveRegeneration(int regenerationAmount, float approximateDuration) {
            float meanInterval = approximateDuration / regenerationAmount;

            for (int i = 0; i < regenerationAmount; i++) {
                float randomInterval = UnityEngine.Random.Range(meanInterval * 0.5f, meanInterval * 1.5f);
                yield return new WaitForSeconds(randomInterval);
                PerformResourceRegeneration(1);
                NotifyRegenerationClientRpc(1);
                Log($"Progressive Regeneration: Regenerated 1 resource with interval {randomInterval}s");
            }
        }
        
        private void PerformResourceRegeneration(int regeneratedAmount) {
            _currentResources.Value += regeneratedAmount;
            Log($"Performed Resource Regeneration: {regeneratedAmount} resources");
        }
        #endregion
        
        #region Extraction Methods
        public bool BeginResourceExtraction(int requestedAmount) {
            if (!CanExtractResources(requestedAmount)) return false;
            StartCoroutine(DelayedExtraction(requestedAmount, 2f)); // Start a Coroutine for delayed extraction
            Log($"Beginning Resource Extraction: {requestedAmount} resources");
            return true;
        }

        private IEnumerator DelayedExtraction(int requestedAmount, float aproximateDuration) {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds
    
            NotifyBeginExtractionProcessClientRpc(requestedAmount);
            yield return StartCoroutine(ProgressiveExtraction(requestedAmount, aproximateDuration));
            
            yield return new WaitForSeconds(2f); // Wait for 2 seconds
            NotifyEndExtractionProcessClientRpc();
            extractionsCompletedEventChannel.Raise(_groupId);
    
            EvaluateExtractionLimit();
            LogResourceExtraction(requestedAmount);
        }

        private IEnumerator ProgressiveExtraction(int requestedAmount, float aproximateDuration) {
            float meanInterval = aproximateDuration / requestedAmount;

            for (int i = 0; i < requestedAmount; i++) {
                float randomInterval = UnityEngine.Random.Range(meanInterval * 0.5f, meanInterval * 1.5f);
                yield return new WaitForSeconds(randomInterval);
                PerformResourceExtraction(1);
                NotifyExtractionClientRpc(1);
                Log($"Progressive Extraction: Extracted 1 resource with interval {randomInterval}s");
            }
        }

        private bool CanExtractResources(int requestedAmount) {
            if (requestedAmount > AvailableExtractions()) {
                Log($"Extraction failed. Requested: {requestedAmount}. Allowed: {AvailableExtractions()}");
                return false;
            }

            if (requestedAmount > _currentResources.Value) {
                Log($"Extraction failed. Requested: {requestedAmount}. Available: {_currentResources.Value}");
                return false;
            }

            return true;
        }

        private int AvailableExtractions() => _remainingExtractionsThisRound - _extractionsThisRound;

        private void PerformResourceExtraction(int extractedAmount) {
            _currentResources.Value -= extractedAmount;
            _extractionsThisRound += extractedAmount;
            Log($"Performed Resource Extraction: {extractedAmount} resources");
        }

        #endregion

        #region Client Rpc Methods
        [ClientRpc]
        private void NotifyBeginExtractionProcessClientRpc(int requestedAmount) {
            Log($"Extraction of {requestedAmount} resources is about to start...");
            beginExtractionProcessEventChannel.Raise();
        }

        [ClientRpc]
        private void NotifyExtractionClientRpc(int extractedAmount) {
            Log($"Extracted: {extractedAmount}");
            extractResourceEventChannel.Raise(extractedAmount);
            extractionsThisRoundReference.Value += extractedAmount;
        }
        
        [ClientRpc]
        private void NotifyEndExtractionProcessClientRpc() {
            Log("End of Extraction Process");
            endExtractionProcessEventChannel.Raise();
        }

        [ClientRpc]
        private void NotifyBeginRegenerationProcessClientRpc(int regenerationAmount) {
            Log($"Regeneration of {regenerationAmount} resources is about to start...");
            beginRegenerationProcessEventChannel.Raise();
            extractionsThisRoundReference.Value = 0;
        }

        [ClientRpc]
        private void NotifyRegenerationClientRpc(int resourcesRegenerated) {
            Log($"Regenerated: {resourcesRegenerated}");
            regenerateResourceEventChannel.Raise(resourcesRegenerated);
        }
        
        [ClientRpc]
        private void NotifyEndRegenerationProcessClientRpc() {
            Log("End of Extraction Process");
            endRegenerationProcessEventChannel.Raise();
        }
        #endregion

        #region Helper Methods
        private void UpdateExtractionLimits() {
            _maxExtractionsPerRound = _totalParticipants * _allowedExtractionsPerParticipant.Value;
            _remainingExtractionsThisRound = _maxExtractionsPerRound;
            Log($"Extraction Limits Updated. Max Per Round: {_maxExtractionsPerRound}, Remaining: {_remainingExtractionsThisRound}");
        }

        private void EvaluateExtractionLimit() {
            if (_extractionsThisRound >= _remainingExtractionsThisRound) {
                Log($"Extraction Limit Reached for This Round: {_extractionsThisRound}/{_remainingExtractionsThisRound}");
                _remainingExtractionsThisRound = 0;
            }
            else {
                _remainingExtractionsThisRound -= _extractionsThisRound;
                Log($"Remaining Extractions This Round: {_remainingExtractionsThisRound}");
            }
            _extractionsThisRound = 0;
        }

        private void Log(string message) {
            Debug.Log($"[CommonPool - Group: {_groupId}] {message}");
        }

        private void LogResourceExtraction(int extractedAmount) {
            Log($"Resources Extracted: {extractedAmount}, Current Resources: {_currentResources.Value}");
        }
        #endregion
        
        #region Cleanup Methods
        public void DeleteCommonPool() {
            Destroy(gameObject);
        }
        #endregion
    }
}