using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {
    
    [CreateAssetMenu(menuName = "Event Channels/2D Vector Channel")]
    public class Vector2EventChannel : BaseEventChannel<Vector2> { }
    
    [System.Serializable]
    public class UnityVector2Event : UnityEvent<Vector2> { }
}