// using CPRE.Framework.DataContainers.Variables;
// using CPRE.Framework.EventSystem.Channels;
// using Unity.Netcode;
// using Unity.VisualScripting;
// using UnityEngine;
//
// namespace CPRE.Scripts.Experiment {
//     public class ParticipantNetworkBehaviour : NetworkBehaviour {
//
//         [SerializeField] private StringReference debugString;
//         
//         [Header("Runtime Sets")]
//         [SerializeField] private ParticipantRuntimeSet participants;
//         [SerializeField] private GroupRuntimeSet groups;
//
//         [Header("Variable References")]
//         [SerializeField] private IntReference participantIDReference;
//         [SerializeField] private IntReference groupIDReference;
//         [SerializeField] private IntReference treatmentTypeReference;
//         
//         [Header("Events")]
//         [SerializeField] private IntEventChannel resourceExtractionEvent;
//         [SerializeField] private VoidEventChannel startExperimentEvent;
//
//         [Header("Network Variables")]
//         public NetworkVariable<ushort> participantID = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
//         public NetworkVariable<ushort> groupID = new NetworkVariable<ushort>();
//         public NetworkVariable<ushort> treatmentType = new NetworkVariable<ushort>();
//
//         public NetworkVariable<int> resourcesExtracted = new NetworkVariable<int>(0);
//         public NetworkVariable<int> resourcesExtractedThisRound = new NetworkVariable<int>(0);
//
//         private Participant _participantData = new Participant();
//
//         public override void OnNetworkSpawn() {
//             base.OnNetworkSpawn();
//             
//             if(IsOwner) {
//                 participantID.Value = (ushort)participantIDReference.Value;
//             }
//             
//             _participantData.participantNetworkObjectReference = this.NetworkObject;
//             participants.Add(_participantData);
//         }
//         private void Update() {
//             _participantData.participantID = participantID.Value;
//             _participantData.groupID = groupID.Value;
//             _participantData.resourcesExtracted = resourcesExtracted.Value;
//             _participantData.resourcesExtractedThisRound = resourcesExtractedThisRound.Value;
//             
//             if (IsOwner) {
//                 groupIDReference.Value = groupID.Value;
//                 treatmentTypeReference.Value = treatmentType.Value;
//             }
//         }
//
//         public void AssignGroupID(ushort id) {
//             groupID.Value = id;
//             _participantData.groupID = id;
//         }
//
//         public void StartExperiment() {
//             if(!IsServer) return;
//             StartExperimentClientRpc();
//         }
//         
//         public void RequestExtraction(int amount) {
//             if (!IsOwner) return;
//             debugString.Value = "Requesting extraction for participant " + participantID.Value + " in group " +
//                                 groupID.Value + " for " + amount + " resources.";
//             RequestExtractionServerRpc(amount);
//         }
//         
//         [ClientRpc]
//         public void StartExperimentClientRpc() {
//             if(!IsOwner) return;
//             startExperimentEvent.Raise();
//         }
//
//         [ServerRpc(RequireOwnership = false)]
//         public void RequestExtractionServerRpc(int amount) {
//             Debug.Log("Requesting extraction for participant " + participantID.Value + " in group " + groupID.Value + " for " + amount + " resources.");
//             foreach (var group in groups.Items) {
//                 if(group.groupID != groupID.Value) continue;
//                 group.OnParticipantRequestedExtraction(participantID.Value, amount);
//             }
//         }
//
//         [ClientRpc]
//         public void ExtractResourcesClientRpc(int amount, ushort group, ushort participant) {
//             if (participantID.Value == participant){
//                 resourcesExtracted.Value += amount;
//                 resourcesExtractedThisRound.Value += amount;
//             }
//             if (!IsOwner) return;
//             if (groupID.Value != group) return;
//             resourceExtractionEvent.Raise(amount);
//         }
//         
//         
//         
//     }
// }