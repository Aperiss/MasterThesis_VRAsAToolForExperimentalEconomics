using System;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {
	
	[CreateAssetMenu(menuName = "Shared Variables/String Variable")]
	public class StringVariable : BaseVariable<string> { }
	
	[Serializable]
	public class StringReference : VariableReference<string> {
		
		public static implicit operator string(StringReference reference) {
			return reference.Value;
		}
		
		public string ConvertToString() {
			return Value;
		}
	}
}