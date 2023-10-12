using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.Setters {
    [CreateAssetMenu(fileName = "New Set Float Action", menuName = "State Machine/Actions/Variable Operations/Set Bool", order = 0)]
    public class SetBoolVariableAction : Action {
        [SerializeField] private BoolReference leftElement;
        [SerializeField] private BoolReference rightElement;

        public override void Act() {
            leftElement.Value = rightElement.Value;
            UnityEngine.Debug.Log("new Value = " + leftElement.Value.ToString());
        }
    }
}