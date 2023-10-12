using System;
using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/String Runtime Set")]
    public class StringRuntimeSet : BaseRuntimeSet<String> {
        public static implicit operator List<String>(StringRuntimeSet set) {
            return set.Items;
        }
    }
}