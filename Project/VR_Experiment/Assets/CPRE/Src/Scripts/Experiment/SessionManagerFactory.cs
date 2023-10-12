using CPRE.Scripts.Experiment.DataContainers;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;
using UnityEngine.Serialization;

namespace CPRE.Scripts.Experiment {
    public class SessionManagerFactory : MonoBehaviour {

        [SerializeField] private SessionManagerRuntimeSet sessionManagerRuntimeSet;
        [SerializeField] private GameObject sessionManagerPrefab;
        
        [Header("Session Manager Parameters")]
        [SerializeField] private VoidEventChannel startExperimentEventChannel;
        [SerializeField] private VoidEventChannel endExperimentEventChannel;
        
        
        public void CreateSessionManager(int groupId) {
            var sessionManagerInstance = GameObject.Instantiate(sessionManagerPrefab, Vector3.zero, Quaternion.identity);
            var sessionManager = sessionManagerInstance.GetComponent<SessionManager>();
            sessionManager.Initialize((ushort) groupId, startExperimentEventChannel, endExperimentEventChannel);
            
            sessionManagerRuntimeSet.Add(sessionManager);
        }
        
        
    }
}