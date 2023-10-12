using System;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {

	[CreateAssetMenu(menuName = "Shared Variables/Float Variable")]
	public class FloatVariable : BaseVariable<float> { }

	[Serializable]
	public class FloatReference : VariableReference<float> {
		
		public static implicit operator float(FloatReference reference) {
			return reference.Value;
		}
		
		public string ConvertToString() {
			return Value.ToString();
		}
	}
}