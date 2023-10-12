using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.TriggerEvent {
    [CreateAssetMenu(fileName = "TriggerEventAction", menuName = "State Machine/Actions/Trigger Event/Trigger Int Event", order = 0)]
    public class TriggerIntEventAction : Action {
        [SerializeField] private IntEventChannel eventChannel;
        [SerializeField] private IntReference eventParameter;

        public override void Act() {
            eventChannel.Raise(eventParameter.Value);
        }
    }
}