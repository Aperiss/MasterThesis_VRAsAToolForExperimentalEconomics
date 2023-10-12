using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Conditions {
    [CreateAssetMenu(fileName = "EventTriggerCondition", menuName = "State Machine/Conditions/Event Triggered Condition", order = 0)]
    public class EventTriggeredCondition : Condition {
        public bool _hasBeenTriggered = false;

        public override bool CheckCondition() {
            return _hasBeenTriggered;
        }

        public void OnEventTriggered() {
            _hasBeenTriggered = true;
        }

        public override void FlushCondition() {
            _hasBeenTriggered = false;
        }
        
        public override void Reset() {
            FlushCondition();
        }
    }
}