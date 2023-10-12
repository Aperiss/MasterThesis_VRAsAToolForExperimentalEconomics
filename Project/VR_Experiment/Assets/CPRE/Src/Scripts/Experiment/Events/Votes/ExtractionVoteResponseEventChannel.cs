using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace CPRE.Scripts.Experiment.Events.Votes {
    public struct ExtractionVoteResponseData {
        public ushort participantId;
        public bool voteResults;
        public bool participantVote;
    }
    
    [CreateAssetMenu(fileName = "EC_ExtractionResponse", menuName = "Event Channels/Experiment/Extraction Vote Response Channel")]
    public class ExtractionVoteResponseEventChannel : BaseEventChannel<ExtractionVoteResponseData> { }
    
    [System.Serializable]
    public class UnityExtractionVoteResponseEvent : UnityEvent<ExtractionVoteResponseData> { }
}