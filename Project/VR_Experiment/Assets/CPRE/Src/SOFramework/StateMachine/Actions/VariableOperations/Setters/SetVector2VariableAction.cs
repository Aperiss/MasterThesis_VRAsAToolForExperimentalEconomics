using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.VariableOperations.Setters {
    [CreateAssetMenu(fileName = "New Set Float Action", menuName = "State Machine/Actions/Variable Operations/Set 2D Vector", order = 0)]
    public class SetVector2VariableAction : Action {
        [SerializeField] private Vector2Reference leftElement;
        [SerializeField] private Vector2Reference rightElement;

        public override void Act() {
            leftElement.Value = rightElement.Value;
        }
    }
}