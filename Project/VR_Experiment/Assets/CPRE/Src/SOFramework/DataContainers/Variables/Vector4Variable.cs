using System;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {
	
    [CreateAssetMenu(menuName = "Shared Variables/4D Vector Variable")]
    public class Vector4Variable : BaseVariable<Vector4> { }
	
    [Serializable]
    public class Vector4Reference : VariableReference<Vector4> {
		
        public static implicit operator Vector4(Vector4Reference reference) {
            return reference.Value;
        }
        
        public string ConvertToString() {
            return Value.ToString();
        }
    }
}