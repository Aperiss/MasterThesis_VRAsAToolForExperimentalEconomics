using System.Collections.Generic;
using CPRE.SOFramework.DataContainers.Sets;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace CPRE.Scripts.Experiment.DataContainers {
    [CreateAssetMenu(menuName = "Runtime Sets/Experiment/Common Pool Runtime Set")]
    public class CommonPoolRuntimeSet : BaseRuntimeSet<CommonPool> {
        public static implicit operator List<CommonPool>(CommonPoolRuntimeSet set) {
            return set.Items;
        }
        
    }
}