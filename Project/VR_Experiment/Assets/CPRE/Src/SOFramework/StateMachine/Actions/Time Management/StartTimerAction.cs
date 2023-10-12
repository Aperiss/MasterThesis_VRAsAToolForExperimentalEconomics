using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.Time_Management {
    [CreateAssetMenu(fileName = "new Start Timer Action", menuName = "State Machine/Actions/Time Management/Start Timer", order = 0)]
    public class StartTimerAction : Action {

        [SerializeField] private FloatReference timer;
        
        public override void Act() {
            timer.Value = 0;
        }
    }
}