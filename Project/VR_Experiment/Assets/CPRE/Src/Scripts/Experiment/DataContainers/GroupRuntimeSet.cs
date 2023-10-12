using System.Collections.Generic;
using CPRE.SOFramework.DataContainers.Sets;
using UnityEngine;

namespace CPRE.Scripts.Experiment.DataContainers {
    [CreateAssetMenu(menuName = "Runtime Sets/Experiment/Group Runtime Set")]
    public class GroupRuntimeSet : BaseRuntimeSet<Group> {
        public static implicit operator List<Group>(GroupRuntimeSet set) {
            return set.Items;
        }
        
    }
}