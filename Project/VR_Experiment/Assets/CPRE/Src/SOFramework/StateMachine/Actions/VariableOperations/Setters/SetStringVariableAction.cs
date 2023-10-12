using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.Setters {
    [CreateAssetMenu(fileName = "New Set Float Action", menuName = "State Machine/Actions/Variable Operations/Set String", order = 0)]
    public class SetStringVariableAction : Action {
        [SerializeField] private StringReference leftElement;
        [SerializeField] private StringReference rightElement;

        public override void Act() {
            leftElement.Value = rightElement.Value;
        }
    }
}