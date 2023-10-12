using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using TMPro;
using UnityEngine;

namespace CPRE.Scripts.VRInteraction {
    public class Extractor : MonoBehaviour{
        [SerializeField] private IntEventChannel requestExtractionEventChannel;
        
        [SerializeField] private IntReference maxExtractionsPerRound;
        [SerializeField] private IntReference resourcesExtractedThisRound;

        [SerializeField] private TextMeshProUGUI screenOutput;
        
        private int _extractionsRequested = 1;
        private int _minimumExtractions = 1;
        
        public void Update() {
            UpdateScreenInformation();
        }
        
        public void IncreaseSelectedAmount() {
            if (_extractionsRequested + resourcesExtractedThisRound < maxExtractionsPerRound) {
                _extractionsRequested++;
            }
        }
        
        public void DecreaseSelectedAmount() {
            if (_extractionsRequested > _minimumExtractions) {
                _extractionsRequested--;
            }
        }
        
        public void RequestExtractResources() {
            var extractions = (_extractionsRequested + resourcesExtractedThisRound <= maxExtractionsPerRound) ? 
                _extractionsRequested : 
                maxExtractionsPerRound - resourcesExtractedThisRound;
            
            requestExtractionEventChannel.Raise(extractions);
            _extractionsRequested = 0;
            _minimumExtractions = 0;
        }

        public void ResetExtractor() {
            _minimumExtractions = 1;
            _extractionsRequested = 1;
        }
        
        private void UpdateScreenInformation() {
            screenOutput.text = _extractionsRequested.ToString() + " / " + (maxExtractionsPerRound - resourcesExtractedThisRound).ToString();
        }
    }
}