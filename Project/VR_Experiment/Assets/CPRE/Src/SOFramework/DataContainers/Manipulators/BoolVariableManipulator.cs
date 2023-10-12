using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Manipulators {
    public class BoolVariableManipulator {
        [SerializeField] private BoolReference boolReference;
        
        public void Set(bool value) => boolReference.Value = value;
        public void Toggle() => boolReference.Value = !boolReference.Value;
    }
}