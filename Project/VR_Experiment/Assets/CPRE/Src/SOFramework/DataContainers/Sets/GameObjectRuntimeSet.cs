using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    
    [CreateAssetMenu(menuName = "Runtime Sets/Game Object Runtime Set")]
    public class GameObejctRuntimeSet : BaseRuntimeSet<GameObject> {
        public static implicit operator List<GameObject>(GameObejctRuntimeSet set) {
            return set.Items;
        }
    }
}