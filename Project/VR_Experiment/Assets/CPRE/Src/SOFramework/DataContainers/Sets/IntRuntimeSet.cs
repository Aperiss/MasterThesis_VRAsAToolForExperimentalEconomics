using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/Integer Runtime Set")]
    public class IntegerRuntimeSet : BaseRuntimeSet<int> {
        public static implicit operator List<int>(IntegerRuntimeSet set) {
            return set.Items;
        }
    }
}