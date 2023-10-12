using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {
    
    [CreateAssetMenu(menuName = "Event Channels/Integer Channel")]
    public class IntEventChannel : BaseEventChannel<int> { }
    
    [System.Serializable]
    public class UnityIntEvent : UnityEvent<int> { }
}