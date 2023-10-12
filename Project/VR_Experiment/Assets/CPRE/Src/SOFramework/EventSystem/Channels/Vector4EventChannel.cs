using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {
    
    [CreateAssetMenu(menuName = "Event Channels/4D Vector Channel")]
    public class Vector4EventChannel : BaseEventChannel<Vector4> { }
    
    [System.Serializable]
    public class UnityVector4Event : UnityEvent<Vector4> { }
}