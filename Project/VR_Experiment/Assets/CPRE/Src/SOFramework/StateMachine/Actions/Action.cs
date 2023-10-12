using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions {
    public abstract class Action : ScriptableObject {
        public abstract void Act();
    }
}