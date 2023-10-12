using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {
    
    [CreateAssetMenu(menuName = "Event Channels/Boolean Channel")]
    public class BoolEventChannel : BaseEventChannel<bool> { }
    
    [System.Serializable]
    public class UnityBoolEvent : UnityEvent<bool> { }
}