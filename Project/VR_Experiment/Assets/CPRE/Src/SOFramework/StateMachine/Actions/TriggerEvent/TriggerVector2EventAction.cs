using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.TriggerEvent {
    [CreateAssetMenu(fileName = "TriggerEventAction", menuName = "State Machine/Actions/Trigger Event/Trigger 2D Vector Event", order = 0)]
    public class TriggerVector2EventAction : Action {
        [SerializeField] private Vector2EventChannel eventChannel;
        [SerializeField] private Vector2Reference eventParameter;

        public override void Act() {
            eventChannel.Raise(eventParameter.Value);
        }
    }
}