using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace CPRE.Scripts.Experiment.Events.Extractions {
    public class ExtractionRequestData {
        public ushort participantId;
        public int extractionAmount;
    }
    
    [CreateAssetMenu(fileName = "EC_ExtractionRequest", menuName = "Event Channels/Experiment/Extraction Request Channel")]
    public class ExtractionRequestEventChannel : BaseEventChannel<ExtractionRequestData> { }
    
    [System.Serializable]
    public class UnityExtractionRequestEvent : UnityEvent<ExtractionRequestData> { }
}