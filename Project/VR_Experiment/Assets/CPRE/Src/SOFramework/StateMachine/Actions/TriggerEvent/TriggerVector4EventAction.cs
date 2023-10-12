using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.TriggerEvent {
    [CreateAssetMenu(fileName = "TriggerEventAction", menuName = "State Machine/Actions/Trigger Event/Trigger 4D Vector Event", order = 0)]
    public class TriggerVector4EventAction : Action {
        [SerializeField] private Vector4EventChannel eventChannel;
        [SerializeField] private Vector4Reference eventParameter;

        public override void Act() {
            eventChannel.Raise(eventParameter.Value);
        }
    }
}