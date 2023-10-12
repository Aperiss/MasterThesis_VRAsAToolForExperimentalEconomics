using System;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {
	
    [CreateAssetMenu(menuName = "Shared Variables/2D Vector Variable")]
    public class Vector2Variable : BaseVariable<Vector2> { }
	
    [Serializable]
    public class Vector2Reference : VariableReference<Vector2> {
		
        public static implicit operator Vector2(Vector2Reference reference) {
            return reference.Value;
        }
        
        public string ConvertToString() {
            return Value.ToString();
        }
    }
}