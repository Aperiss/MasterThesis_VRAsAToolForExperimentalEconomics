using System.Linq;
using CPRE.Scripts.Experiment.DataContainers;
using CPRE.Scripts.Experiment.Events;
using CPRE.Scripts.Experiment.Events.Extractions;
using CPRE.Scripts.Experiment.Events.Votes;
using UnityEngine;

namespace CPRE.Scripts.Experiment {
    public class GroupEventObserver : MonoBehaviour {

        [SerializeField] private GroupRuntimeSet groups;

        public void OnParticipantRequestedExtraction(ExtractionRequestData request) {
            var group = GetGroupByParticipantID(request.participantId);
            if (group == null) return;
            group.OnParticipantRequestedExtraction(request.participantId, request.extractionAmount);
        }
        
        public void OnParticipantVotedForExtraction(ExtractionVoteRequestData request) {
            var group = GetGroupByParticipantID(request.participantId);
            if (group == null) return;
            group.OnParticipantVotedForExtraction(request.participantId, request.vote);
        }
        
        public void OnParticipantFinishedManufacturing(int participantId) {
            var group = GetGroupByParticipantID((ushort)participantId);
            if (group == null) return;
            group.OnParticipantFinishedManufacturing((ushort)participantId);
        }
        
        private Group GetGroupByParticipantID(ushort participantID) {
            foreach (var group in groups.Items) {
                if(group.ContainsParticipant(participantID)) return group;
            }
            return null;
        }

        public void OnCommonPoolProcessedExtractions(int groupId) {
            Debug.Log($"OnCommonPoolProcessedExtractions called with groupId: {groupId}.");
            var group = groups.Items.FirstOrDefault(g => g.GroupId == groupId);
    
            if (group == null) {
                Debug.Log($"Group with groupId: {groupId} not found.");
                return;
            }

            Debug.Log($"Group with groupId: {groupId} found. Notifying participants of individual extractions.");
            group.NotifyParticipantsOfIndividualExtractions();
        }
        
        public void OnRoundEndNotified(int groupId) {
            Debug.Log($"OnRoundEndNotified called with groupId: {groupId}.");
            var group = groups.Items.FirstOrDefault(g => g.GroupId == groupId);
    
            if (group == null) {
                Debug.Log($"Group with groupId: {groupId} not found.");
                return;
            }

            Debug.Log($"Group with groupId: {groupId} found. Notifying group of round end.");
            group.StartNewRound();
        }
        
    }
}