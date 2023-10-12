using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.Setters {
    [CreateAssetMenu(fileName = "New Set Float Action", menuName = "State Machine/Actions/Variable Operations/Set 4D Vector", order = 0)]
    public class SetVector4VariableAction : Action {
        [SerializeField] private Vector4Reference leftElement;
        [SerializeField] private Vector4Reference rightElement;

        public override void Act() {
            leftElement.Value = rightElement.Value;
        }
    }
}