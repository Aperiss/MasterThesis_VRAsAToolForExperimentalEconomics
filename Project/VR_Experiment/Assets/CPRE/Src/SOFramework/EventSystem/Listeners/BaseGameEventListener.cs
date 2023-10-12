using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace CPRE.SOFramework.EventSystem.Listeners {
    public interface IGameEventListener<T> {
        public void OnEventRaised(T item);
    }
    
    public class BaseGameEventListener<T, E, UER> : MonoBehaviour, 
        IGameEventListener<T> where E : BaseEventChannel<T> where UER : UnityEvent<T> {

        [SerializeField] protected E eventChannel;
        [SerializeField] protected UER unityEventResponse;

        private void OnEnable() {
            if (eventChannel != null) {
                eventChannel.RegisterListener(this);
            }
        }

        private void OnDisable() {
            if (eventChannel) {
                eventChannel.UnregisterListener(this);
            }
        }

        public void OnEventRaised(T item) {
            if (unityEventResponse != null) {
                unityEventResponse.Invoke(item);
            }
        }
    }
}