using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.TriggerEvent {
    [CreateAssetMenu(fileName = "TriggerEventAction", menuName = "State Machine/Actions/Trigger Event/Trigger Float Event", order = 0)]
    public class TriggerFloatEventAction : Action {
        [SerializeField] private FloatEventChannel eventChannel;
        [SerializeField] private FloatReference eventParameter;

        public override void Act() {
            eventChannel.Raise(eventParameter.Value);
        }
    }
}