using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace CPRE.Scripts.Experiment.Events.Extractions {
    public struct ExtractionResponseData {
        public ushort participantId;
        public int extractionAmount;
        public bool isApproved;
    }
    
    [CreateAssetMenu(fileName = "EC_ExtractionResponse", menuName = "Event Channels/Experiment/Extraction Response Channel")]
    public class ExtractionResponseEventChannel : BaseEventChannel<ExtractionResponseData> { }
    
    [System.Serializable]
    public class UnityExtractionResponseEvent : UnityEvent<ExtractionResponseData> { }
}