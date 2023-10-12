using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.TriggerEvent {
    [CreateAssetMenu(fileName = "TriggerVoidEventAction", menuName = "State Machine/Actions/Trigger Event/Trigger Void Event", order = 0)]
    public class TriggerVoidEventAction : Action {
        [SerializeField] private VoidEventChannel eventChannel;

        public override void Act() {
            eventChannel.Raise();
        }
    }
}