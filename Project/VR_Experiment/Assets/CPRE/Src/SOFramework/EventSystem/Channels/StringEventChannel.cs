using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {
    
    [CreateAssetMenu(menuName = "Event Channels/String Channel")]
    public class StringEventChannel : BaseEventChannel<string> { }
    
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
}