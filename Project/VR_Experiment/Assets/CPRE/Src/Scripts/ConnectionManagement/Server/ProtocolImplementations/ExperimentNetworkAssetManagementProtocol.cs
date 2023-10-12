using CPRE.Scripts.ConnectionManagement.Server.Protocols;
using CPRE.Scripts.Experiment.DataContainers;
using UnityEngine;

namespace CPRE.Scripts.ConnectionManagement.Server.ProtocolImplementations {
    [CreateAssetMenu(fileName = "SCB_NetworkObjectManager", menuName = "Networking/Experiment/NetworkObjectManager")]
    public class ExperimentNetworkAssetManagementProtocol : ScriptableObject, INetworkAssetManagementProtocol {
        [SerializeField] private GroupRuntimeSet groups;

        public void SpawnNetworkObjects() {
            foreach (var group in groups.Items) {
                var commonPool = group.GetCommonPool();
                if (commonPool != null) {
                    var commonPoolNetworkObject = commonPool.NetworkObject;
                    if (commonPoolNetworkObject != null) {
                        commonPoolNetworkObject.SpawnWithObservers = false;
                        commonPoolNetworkObject.Spawn(false);
                        foreach (var participant in group.GetParticipantsInGroup()) {
                            commonPoolNetworkObject.NetworkShow(participant.networkClientId);
                        }
                    }
                }
                
                var sessionManager = group.GetSessionManager();
                if (sessionManager != null) {
                    var sessionManagerNetworkObject = sessionManager.NetworkObject;
                    if (sessionManagerNetworkObject != null) {
                        sessionManagerNetworkObject.SpawnWithObservers = false;
                        sessionManagerNetworkObject.Spawn(false);
                        foreach (var participant in group.GetParticipantsInGroup()) {
                            sessionManagerNetworkObject.NetworkShow(participant.networkClientId);
                        }
                    }
                }
                
                foreach (var participant in group.GetParticipantsInGroup()) {
                    var participantNetworkObject = participant.NetworkObject;
                    if (participantNetworkObject != null) {
                        participantNetworkObject.SpawnWithObservers = false;
                        participantNetworkObject.SpawnWithOwnership(participant.networkClientId);
                        participantNetworkObject.NetworkShow(participant.networkClientId);
                    }
                }
            }
        }

        public void DestroyNetworkObjects() {
            foreach (var group in groups.Items) {
                var commonPool = group.GetCommonPool();
                if (commonPool != null) {
                    var commonPoolNetworkObject = commonPool.NetworkObject;
                    if (commonPoolNetworkObject != null) {
                        commonPoolNetworkObject.Despawn();
                    }
                }

                var sessionManager = group.GetSessionManager();
                if (sessionManager != null) {
                    var sessionManagerNetworkObject = sessionManager.NetworkObject;
                    if (sessionManagerNetworkObject != null) {
                        sessionManagerNetworkObject.Despawn();
                    }
                }

                foreach (var participant in group.GetParticipantsInGroup()) {
                    var participantNetworkObject = participant.NetworkObject;
                    if (participantNetworkObject != null) {
                        participantNetworkObject.Despawn();
                    }
                }
            }
        }
    }
}