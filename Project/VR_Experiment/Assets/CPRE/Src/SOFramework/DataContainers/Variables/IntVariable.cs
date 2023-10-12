using System;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {
	
	[CreateAssetMenu(menuName = "Shared Variables/Integer Variable")]
	public class IntVariable : BaseVariable<int> { }

	[Serializable]
	public class IntReference : VariableReference<int> {
		
		public static implicit operator int(IntReference reference) {
			return reference.Value;
		}
		
		public string ConvertToString() {
			return Value.ToString();
		}
	}
}