using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Channels {
    
    [CreateAssetMenu(menuName = "Event Channels/3D Vector Channel")]
    public class Vector3EventChannel : BaseEventChannel<Vector3> { }
    
    [System.Serializable]
    public class UnityVector3Event : UnityEvent<Vector3> { }
}