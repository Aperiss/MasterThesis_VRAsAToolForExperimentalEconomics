using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.Time_Management {
    [CreateAssetMenu(fileName = "new Update Timer Action", menuName = "State Machine/Actions/Time Management/Update Timer", order = 0)]
    public class UpdateTimerAction : Action {
        
        [SerializeField] private FloatReference timer;
        
        public override void Act() {
            timer.Value += Time.deltaTime;
        }
    }
}