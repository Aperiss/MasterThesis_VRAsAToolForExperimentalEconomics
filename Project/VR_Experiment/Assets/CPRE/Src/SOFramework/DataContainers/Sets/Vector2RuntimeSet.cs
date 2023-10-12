using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/2D Vector Runtime Set")]
    public class Vector2RuntimeSet : BaseRuntimeSet<Vector2> {
        public static implicit operator List<Vector2>(Vector2RuntimeSet set) {
            return set.Items;
        }
    }
}