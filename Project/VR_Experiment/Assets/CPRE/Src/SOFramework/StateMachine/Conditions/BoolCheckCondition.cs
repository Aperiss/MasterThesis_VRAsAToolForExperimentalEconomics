using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Conditions {
    [CreateAssetMenu(fileName = "new Comparison", menuName = "Pluggable State Machine/Conditions/BooleanReference Check Condition", order = 0)]
    public class BoolCheckCondition : Condition{
        public BoolReference _boolToCheck;
        public override bool CheckCondition() {
            return _boolToCheck.Value;
        }

        public override void FlushCondition() {
            _boolToCheck.Value = false;
        }
        
        public override void Reset() {
            
        }
    }
}