using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {
    
    [CreateAssetMenu(menuName = "Event Channels/Float Channel")]
    public class FloatEventChannel : BaseEventChannel<float> { }
    
    [System.Serializable]
    public class UnityFloatEvent : UnityEvent<float> { }
}