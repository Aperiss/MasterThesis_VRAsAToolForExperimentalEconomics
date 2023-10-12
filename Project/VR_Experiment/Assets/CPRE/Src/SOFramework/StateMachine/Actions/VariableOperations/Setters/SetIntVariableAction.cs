using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.Setters {
    [CreateAssetMenu(fileName = "New Set Float Action", menuName = "State Machine/Actions/Variable Operations/Set Int", order = 0)]
    public class SetIntVariableAction : Action {
        [SerializeField] private IntReference leftElement;
        [SerializeField] private IntReference rightElement;

        public override void Act() {
            leftElement.Value = rightElement.Value;
        }
    }
}