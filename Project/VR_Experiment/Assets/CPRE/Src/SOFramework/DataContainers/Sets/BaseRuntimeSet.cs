using System;
using System.Collections.Generic;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Sets {
    public abstract class BaseRuntimeSet<T> : ScriptableObject
    {
        [SerializeField] private bool resetAtRuntime;
        [SerializeField] private List<T> items = new List<T>();
        
        public List<T> Items {
            get => items;
            set => items = value;
        }

        public void Add(T item) {
            items.Add(item);
        }
        
        public void Remove(T item) {
            if (items.Contains(item)) {
                items.Remove(item);
            }
        }

        public void RemoveAt(int index) {
            if (index >= 0 && index < items.Count) {
                items.RemoveAt(index);
            }
            else {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void Clear() {
            items.Clear();
        }
        
        public void OnEnable() {
            if (resetAtRuntime) {
                items.Clear();
            }
        }
    }
}
