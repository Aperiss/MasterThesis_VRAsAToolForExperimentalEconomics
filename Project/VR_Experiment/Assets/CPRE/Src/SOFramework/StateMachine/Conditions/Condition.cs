using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Conditions {
    public abstract class Condition : ScriptableObject {
        public abstract bool CheckCondition();
        public abstract void FlushCondition();

        public abstract void Reset();
    }
}