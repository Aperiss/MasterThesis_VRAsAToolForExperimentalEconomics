using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.BasicArithmetic {
    [CreateAssetMenu(fileName = "New Float-Int Arithmetic Operation", menuName = "State Machine/Actions/Variable Operations/Int-Float Arithmetic Operation", order = 0)]
    public class IntFloatArithmeticOperationAction : Action {
        public enum ArithmeticOperationType {
            Addition,
            Subtraction,
            Multiplication,
            Division,
        }

        [SerializeField] private ArithmeticOperationType calculationType = ArithmeticOperationType.Addition;
        [SerializeField] private IntReference leftElement;
        [SerializeField] private FloatReference rightElement;

        public override void Act() {
            switch (calculationType) {
                case ArithmeticOperationType.Addition:
                    leftElement.Value += (int)rightElement.Value;
                    break;
                case ArithmeticOperationType.Subtraction:
                    leftElement.Value -= (int)rightElement.Value;
                    break;
                case ArithmeticOperationType.Multiplication:
                    leftElement.Value *= (int)rightElement.Value;
                    break;
                case ArithmeticOperationType.Division:
                    leftElement.Value /= (int)rightElement.Value;
                    break;
            }
        }
    }
}