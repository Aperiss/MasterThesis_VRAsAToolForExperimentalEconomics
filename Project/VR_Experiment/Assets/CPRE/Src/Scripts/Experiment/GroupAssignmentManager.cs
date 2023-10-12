using System.Linq;
using CPRE.Scripts.Experiment.DataContainers;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.Scripts.Experiment {

    public class GroupAssignmentManager : MonoBehaviour {

        [SerializeField] private IntReference maxGroupSize;

        [Header("Runtime Sets")]
        [SerializeField] private GroupRuntimeSet groups;
        [SerializeField] private ParticipantRuntimeSet participants;
        [SerializeField] private CommonPoolRuntimeSet commonPools;
        [SerializeField] private SessionManagerRuntimeSet sessionManagers;
        
        private void Update() {
            foreach(var participant in participants.Items) {
                if (participant.groupID.Value == 0) {
                    RemoveParticipantFromGroups(participant.participantID.Value);
                    continue;
                }
                var participantGroup = GetGroupByParticipantID(participant.participantID.Value);
                if (participantGroup != null && participantGroup.GroupId == participant.groupID.Value) continue;
                RemoveParticipantFromGroups(participant.participantID.Value);
                AddParticipantToGroup(participant.participantID.Value, participant.groupID.Value);
            }
            
            foreach (var group in groups.Items) {
                if (group.GetCommonPool() != null) continue;
                var commonPool = commonPools.Items.FirstOrDefault(cp => cp.GroupId == group.GroupId);
                if (commonPool == null) continue;
                group.AssignCommonPool(commonPool);
            }
            
            foreach (var group in groups.Items) {
                if (group.GetSessionManager() != null) continue;
                var sessionManager = sessionManagers.Items.FirstOrDefault(sm => sm.GroupId == group.GroupId);
                if (sessionManager == null) continue;
                group.AssignSessionManager(sessionManager);
            }
        }

        private bool AddParticipantToGroup(ushort participantID, ushort groupID) {
            var group = groups.Items.FirstOrDefault(g => g.GroupId == groupID);
            if (group == null || group.GroupSize >= maxGroupSize) return false;

            var participant = participants.Items.FirstOrDefault(p => p.participantID.Value == participantID);
            if (participant == null) return false;
    
            group.AddParticipant(participantID, participant);
            return true;
        }
        
        private void RemoveParticipantFromGroups(ushort participantID) {
            foreach (var group in groups.Items) {
                group.RemoveParticipant(participantID);
            }
        }

        private Group GetGroupByParticipantID(ushort participantID) {
            foreach (var group in groups.Items) {
                if(group.ContainsParticipant(participantID)) return group;
            }

            return null;
        }
    }
}