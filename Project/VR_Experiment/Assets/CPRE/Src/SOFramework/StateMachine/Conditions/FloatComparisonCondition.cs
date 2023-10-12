using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Conditions {
    [CreateAssetMenu(fileName = "new Comparison", menuName = "Pluggable State Machine/Conditions/Float Comparison Condition", order = 0)]
    public class FloatComparisonCondition : Condition {
        public enum ComparisonType {
            SmallerThan,
            SmallerOrEqualThan,
            Equals,
            GreaterThan,
            GreaterOrEqualThan,
        }

        [SerializeField] private ComparisonType comparisonType = ComparisonType.Equals;
        [SerializeField] private FloatReference leftElement;
        [SerializeField] private FloatReference rightElement;

        public override bool CheckCondition() {
            switch (comparisonType) {
                case ComparisonType.SmallerThan:
                    return leftElement.Value < rightElement.Value;
                case ComparisonType.SmallerOrEqualThan:
                    return leftElement.Value <= rightElement.Value;
                case ComparisonType.Equals:
                    return leftElement.Value == rightElement.Value;
                case ComparisonType.GreaterThan:
                    return leftElement.Value > rightElement.Value;
                case ComparisonType.GreaterOrEqualThan:
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