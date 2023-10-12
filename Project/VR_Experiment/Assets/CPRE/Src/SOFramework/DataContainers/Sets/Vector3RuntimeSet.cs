using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/3D Vector Runtime Set")]
    public class Vector3RuntimeSet : BaseRuntimeSet<Vector3> {
        public static implicit operator List<Vector3>(Vector3RuntimeSet set) {
            return set.Items;
        }
    }
}