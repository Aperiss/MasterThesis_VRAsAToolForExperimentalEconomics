using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/Boolean Runtime Set")]
    public class BoolRuntimeSet : BaseRuntimeSet<bool> {
        public static implicit operator List<bool>(BoolRuntimeSet set) {
            return set.Items;
        }
    }
}