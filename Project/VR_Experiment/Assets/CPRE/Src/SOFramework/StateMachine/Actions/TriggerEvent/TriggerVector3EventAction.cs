using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.TriggerEvent {
    [CreateAssetMenu(fileName = "TriggerEventAction", menuName = "State Machine/Actions/Trigger Event/Trigger 3D Vector Event", order = 0)]
    public class TriggerVector3EventAction : Action {
        [SerializeField] private Vector3EventChannel eventChannel;
        [SerializeField] private Vector3Reference eventParameter;

        public override void Act() {
            eventChannel.Raise(eventParameter.Value);
        }
    }
}