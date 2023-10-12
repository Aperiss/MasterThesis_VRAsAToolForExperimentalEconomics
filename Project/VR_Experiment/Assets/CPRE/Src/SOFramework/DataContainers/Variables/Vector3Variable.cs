using System;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {
	
    [CreateAssetMenu(menuName = "Shared Variables/3D Vector Variable")]
    public class Vector3Variable : BaseVariable<Vector3> { }
	
    [Serializable]
    public class Vector3Reference : VariableReference<Vector3> {
		
        public static implicit operator Vector3(Vector3Reference reference) {
            return reference.Value;
        }
        
        public string ConvertToString() {
            return Value.ToString();
        }
    }
}