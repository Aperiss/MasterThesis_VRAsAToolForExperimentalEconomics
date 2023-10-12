using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Manipulators {
    public class StringVariableManipulator : MonoBehaviour {
        [SerializeField] private StringReference stringReference ;

        public void Set(string a) => stringReference.Value = a;
        
        public void ConcatenateToString(string a) => stringReference.Value += a;
        public void ConcatenateToString(int a) => stringReference.Value += a;
        public void ConcatenateToString(float a) => stringReference.Value += a;

        public void RemoveLastChar() {
            if (!string.IsNullOrEmpty(stringReference.Value)) {
                stringReference.Value = stringReference.Value.Remove(stringReference.Value.Length - 1);
            }
        }
        public void RemoveLastChars(int amount) {
            for (int i = 0; i < amount; i++) {
                if (!string.IsNullOrEmpty(stringReference.Value)) {
                    stringReference.Value = stringReference.Value.Remove(stringReference.Value.Length - i);
                }
            }
        }
    }
}
