using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Manipulators {
    public class IntegerVariableManipulator : MonoBehaviour  {
        [SerializeField] private IntReference intReference;
        
        [Header("Optional String Reference To Parse")]
        [SerializeField] private StringReference stringReference;
    
        public void Add(int amount) => intReference.Value += amount;
        public void Subtract(int amount) => intReference.Value -= amount;
        public void Multiply(int amount) => intReference.Value *= amount;
        public void Divide(int amount) => intReference.Value /= amount;
        public void Modulo(int amount) => intReference.Value %= amount;
        public void Set(int amount) => intReference.Value = amount;
        public void Parse(string amount) => intReference.Value = int.Parse(amount);
        public void Parse() => intReference.Value = int.Parse(stringReference.Value);
    }
}
