using System.Collections.Generic;
using CPRE.SOFramework.DataContainers.Sets;
using UnityEngine;

namespace CPRE.Scripts.Experiment.DataContainers {
    [CreateAssetMenu(menuName = "Runtime Sets/Experiment/Session Manager Runtime Set")]
    public class SessionManagerRuntimeSet : BaseRuntimeSet<SessionManager> {
        public static implicit operator List<SessionManager>(SessionManagerRuntimeSet set) {
            return set.Items;
        }
    }
}