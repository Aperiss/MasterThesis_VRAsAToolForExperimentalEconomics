using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/4D Vector Runtime Set")]
    public class Vector4RuntimeSet : BaseRuntimeSet<Vector4> {
        public static implicit operator List<Vector4>(Vector4RuntimeSet set) {
            return set.Items;
        }
    }
}