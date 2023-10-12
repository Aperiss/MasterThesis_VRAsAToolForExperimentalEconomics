using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Variables {

    
    public class BaseVariable<T> : ScriptableObject {
        [SerializeField] private T initialValue;
        private T _runtimeValue;

        private void OnEnable() {
            _runtimeValue = initialValue;
        }

        public T Value {
            get => _runtimeValue;
            set => _runtimeValue = value;
        }
    }

    public abstract class VariableReference<T> {
        [SerializeReference] private bool _useDirect = true;
        [SerializeReference] private T _directValue;
        [SerializeReference] private BaseVariable<T> _variableReference;
        
        protected VariableReference(){}

        protected VariableReference(T value) {
            _useDirect = true;
            _directValue = value;
        }

        public T Value {
            get => _useDirect ? _directValue : _variableReference.Value;
            set {
                if (!_useDirect && !_variableReference.IsUnityNull()) {
                    _variableReference.Value = value;
                }
            }
        }
    }
}