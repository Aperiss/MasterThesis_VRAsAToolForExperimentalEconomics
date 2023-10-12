using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {

    [System.Serializable]
    public struct VoidType { }
    
    [CreateAssetMenu(menuName = "Event Channels/Void Channel")]
    public class VoidEventChannel : BaseEventChannel<VoidType> {
        public void Raise() => Raise(new VoidType());
    }
    
    [System.Serializable]
    public class UnityVoidEvent : UnityEvent<VoidType> { }
}