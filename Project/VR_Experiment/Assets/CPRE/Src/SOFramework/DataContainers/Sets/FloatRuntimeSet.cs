using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/Float Runtime Set")]
    public class FloatRuntimeSet : BaseRuntimeSet<float> {
        public static implicit operator List<float>(FloatRuntimeSet set) {
            return set.Items;
        }
    }
}