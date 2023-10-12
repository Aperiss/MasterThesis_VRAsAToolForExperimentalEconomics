using System.Collections;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine {
    public class StateController : MonoBehaviour {
        public State currentState;
        public State remainState;

        public float transitionDuration;

        private void Update() {
            currentState.UpdateState();
            State nextState = currentState.CheckTransitions(remainState);
            StartCoroutine(TransitionToState(nextState));
        }

        IEnumerator TransitionToState(State nextState) {
            yield return new WaitForSeconds(transitionDuration);
            if (nextState != remainState && nextState != currentState) {
                currentState.ExitState();
                currentState = nextState;
                currentState.EnterState();
            }
        }
    }
}