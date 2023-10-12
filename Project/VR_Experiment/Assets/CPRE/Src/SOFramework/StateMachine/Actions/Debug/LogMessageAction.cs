using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.Debug {
    [CreateAssetMenu(fileName = "NewDebugAction", menuName = "State Machine/Actions/Debug/Log Message", order = 0)]
    public class LogMessageAction : Action {
        [SerializeField] private StringReference message;

        public override void Act() {
            UnityEngine.Debug.Log(message.Value);
        }
    }
}