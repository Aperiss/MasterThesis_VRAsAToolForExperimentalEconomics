using CPRE.SOFramework.StateMachine.Conditions;

namespace CPRE.SOFramework.StateMachine.Transitions {
    [System.Serializable]
    public class Transition {
        public Condition[] conditions;
        public State trueState;
        public State falseState;
        
        public bool CheckAllConditions() {
            foreach (var condition in conditions) {
                if (!condition.CheckCondition()) {
                    return false;
                }
            }
            return true;
        }

        public void FlushAllConditions() {
            foreach (var condition in conditions) {
                condition.FlushCondition();
            }
        }
        
        public void FlushEventConditions() {
            foreach (var condition in conditions) {
                if (condition is EventTriggeredCondition) {
                    condition.FlushCondition();
                }
            }
        }
    }
}