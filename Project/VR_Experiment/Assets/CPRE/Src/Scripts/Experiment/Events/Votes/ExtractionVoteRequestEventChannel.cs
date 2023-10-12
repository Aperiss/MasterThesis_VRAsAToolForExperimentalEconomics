using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace CPRE.Scripts.Experiment.Events.Votes {
    public class ExtractionVoteRequestData {
        public ushort participantId;
        public bool vote;
    }
    
    [CreateAssetMenu(fileName = "EC_ExtractionRequest", menuName = "Event Channels/Experiment/Extraction Vote Request Channel")]
    public class ExtractionVoteRequestEventChannel : BaseEventChannel<ExtractionVoteRequestData> { }
    
    [System.Serializable]
    public class UnityExtractionVoteRequestEvent : UnityEvent<ExtractionVoteRequestData> { }
}