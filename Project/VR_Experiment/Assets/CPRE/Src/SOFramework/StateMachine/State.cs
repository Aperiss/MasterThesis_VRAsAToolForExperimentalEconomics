using CPRE.SOFramework.StateMachine.Actions;
using CPRE.SOFramework.StateMachine.Transitions;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine {
    [CreateAssetMenu(fileName = "State", menuName = "State Machine/State")]
    public class State : ScriptableObject {

        [SerializeField] private Action[] entryActions;
        [SerializeField] private Action[] stateActions;
        [SerializeField] private Action[] exitActions;
        [SerializeField] private Transition[] transitions;
        
        public void EnterState() {
            ResetTransitions();
            ExecuteActions(entryActions);
        }
        
        public void UpdateState() {
            ExecuteActions(stateActions);
        }

        public void ExitState() {
            ExecuteActions(exitActions);
        }

        public State CheckTransitions(State remainState) {
            foreach (var transition in transitions) {
                bool conditionsMet = transition.CheckAllConditions();
                State nextState = conditionsMet ? transition.trueState : transition.falseState;
                if (nextState != remainState) {
                    transition.FlushAllConditions();
                    return nextState;
                }
            }
            FlushAllTransitions();
            return remainState;
        }

        private void ResetTransitions() {
            foreach (var transition in transitions) {
                if (transition == null || transition.conditions == null) continue;
                Debug.Log("Resetting Conditions");
                foreach (var condition in transition.conditions) {
                    condition?.Reset();
                }
            }
        }

        private void ExecuteActions(Action[] actions) {
            foreach (var action in actions) {
                action?.Act();
            }
        }

        private void FlushAllTransitions() {
            foreach (var transition in transitions) {
                transition?.FlushAllConditions();
            }
        }
    }
}