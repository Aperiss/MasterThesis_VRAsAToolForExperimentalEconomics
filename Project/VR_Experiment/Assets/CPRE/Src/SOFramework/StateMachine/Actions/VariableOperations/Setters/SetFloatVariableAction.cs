using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.Setters {
    [CreateAssetMenu(fileName = "New Set Float Action", menuName = "State Machine/Actions/Variable Operations/Set Float", order = 0)]
    public class SetFloatVariableAction : Action {
        [SerializeField] private FloatReference leftElement;
        [SerializeField] private FloatReference rightElement;

        public override void Act() {
            leftElement.Value = rightElement.Value;
        }
    }
}