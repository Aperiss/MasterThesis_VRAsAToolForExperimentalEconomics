﻿using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.BasicArithmetic {
    [CreateAssetMenu(fileName = "New Float Arithmetic Operation", menuName = "State Machine/Actions/Variable Operations/Float-Float Arithmetic Operation", order = 0)]
    public class FloatArithmeticOperationAction : Action {
        public enum ArithmeticOperationType {
            Addition,
            Subtraction,
            Multiplication,
            Division,
        }

        [SerializeField] private ArithmeticOperationType calculationType = ArithmeticOperationType.Addition;
        [SerializeField] private FloatReference leftElement;
        [SerializeField] private FloatReference rightElement;

        public override void Act() {
            switch (calculationType) {
                case ArithmeticOperationType.Addition:
                    leftElement.Value += rightElement.Value;
                    break;
                case ArithmeticOperationType.Subtraction:
                    leftElement.Value -= rightElement.Value;
                    break;
                case ArithmeticOperationType.Multiplication:
                    leftElement.Value *= rightElement.Value;
                    break;
                case ArithmeticOperationType.Division:
                    leftElement.Value /= rightElement.Value;
                    break;
            }
        }
    }
}