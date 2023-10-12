using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.Setters {
    [CreateAssetMenu(fileName = "New Set Float Action", menuName = "State Machine/Actions/Variable Operations/Set 3D Vector", order = 0)]
    public class SetVector3VariableAction : Action {
        [SerializeField] private Vector3Reference leftElement;
        [SerializeField] private Vector3Reference rightElement;

        public override void Act() {
            leftElement.Value = rightElement.Value;
        }
    }
}