using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.Debug {
    [CreateAssetMenu(fileName = "NewDebugAction", menuName = "State Machine/Actions/Debug/Log Int Message", order = 0)]
    public class LogIntMessageAction : Action {
        [SerializeField] private StringReference message;
        [SerializeField] private IntReference number;

        public override void Act() {
            UnityEngine.Debug.Log(message.Value + number.Value.ToString());
        }
    }
}