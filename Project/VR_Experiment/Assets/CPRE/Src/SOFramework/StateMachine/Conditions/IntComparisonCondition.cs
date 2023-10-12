using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Conditions {
    [CreateAssetMenu(fileName = "new Comparison", menuName = "Pluggable State Machine/Conditions/Int Comparison Condition", order = 0)]
    public class IntComparisonCondition : Condition {
        public enum ComparisonType {
            SmallerThan,
            SmallerOrEqualThan,
            Equals,
            GreaterThan,
            GreaterOrEqualThan,
        }

        [SerializeField] private ComparisonType comparisonType = ComparisonType.Equals;
        [SerializeField] private IntReference leftElement;
        [SerializeField] private IntReference rightElement;

        public override bool CheckCondition() {
            switch (comparisonType) {
                case ComparisonType.SmallerThan:
                    Debug.Log(leftElement.Value + " < " + rightElement.Value);
                    Debug.Log(leftElement.Value < rightElement.Value);
                    return leftElement.Value < rightElement.Value;
                case ComparisonType.SmallerOrEqualThan:
                    Debug.Log(leftElement.Value + " <= " + rightElement.Value);
                    Debug.Log(leftElement.Value <= rightElement.Value);
                    return leftElement.Value <= rightElement.Value;
                case ComparisonType.Equals:
                    Debug.Log(leftElement.Value + " = " + rightElement.Value);
                    Debug.Log(leftElement.Value == rightElement.Value);
                    return leftElement.Value == rightElement.Value;
                case ComparisonType.GreaterThan:
                    Debug.Log(leftElement.Value + " > " + rightElement.Value);
                    Debug.Log(leftElement.Value > rightElement.Value);
                    return leftElement.Value > rightElement.Value;
                case ComparisonType.GreaterOrEqualThan:
                    Debug.Log(leftElement.Value + " >= " + rightElement.Value);
                    Debug.Log(leftElement.Value >= rightElement.Value);
                    return leftElement.Value >= rightElement.Value;
                default:
                    return false;
            }
        }
        
        public override void FlushCondition() {
            
        }
        
        public override void Reset() {
            
        }
    }
}