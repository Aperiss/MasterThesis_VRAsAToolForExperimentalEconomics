using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.TriggerEvent {
    [CreateAssetMenu(fileName = "TriggerVoidEventAction", menuName = "State Machine/Actions/Trigger Event/Trigger Bool Event", order = 0)]
    public class TriggerBoolEventAction : Action {
        [SerializeField] private BoolEventChannel eventChannel;
        [SerializeField] private BoolReference eventParameter;

        public override void Act() {
            eventChannel.Raise(eventParameter.Value);
        }
    }
}