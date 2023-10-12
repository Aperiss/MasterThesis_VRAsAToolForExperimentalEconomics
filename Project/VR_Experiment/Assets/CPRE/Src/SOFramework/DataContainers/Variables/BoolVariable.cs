using System;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {
    
    [CreateAssetMenu(menuName = "Shared Variables/Boolean Variable")]
    public class BoolVariable : BaseVariable<bool> {}

    [Serializable]
    public class BoolReference : VariableReference<bool> {
        public static implicit operator bool(BoolReference reference) {
            return reference.Value;
        }
        
        public string ConvertToString() {
            return Value.ToString();
        }
    }
}