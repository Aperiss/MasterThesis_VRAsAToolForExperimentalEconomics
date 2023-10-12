using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.Time_Management {
    [CreateAssetMenu(fileName = "new End Timer Action", menuName = "State Machine/Actions/Time Management/End Timer", order = 0)]
    public class EndTimerAction : Action {

        [SerializeField] private FloatReference timer;
        [SerializeField] private FloatReference timeAtStop;
        
        public override void Act() {
            timeAtStop.Value = timer.Value;
            timer.Value = 0;
        }
    }
}