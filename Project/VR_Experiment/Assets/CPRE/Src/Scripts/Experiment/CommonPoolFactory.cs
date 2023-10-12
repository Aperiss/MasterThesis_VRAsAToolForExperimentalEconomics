using CPRE.Scripts.Experiment.DataContainers;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;
using UnityEngine.Serialization;

namespace CPRE.Scripts.Experiment {
    public class CommonPoolFactory : MonoBehaviour {

        [SerializeField] private CommonPoolRuntimeSet commonPools;
        [SerializeField] private GameObject commonPoolPrefab;
        
        [Header("Common Pool Parameters")]
        [SerializeField] private IntReference commonPoolMaxResources;
        [SerializeField] private IntReference commonPoolInitialResources;
        [SerializeField] private IntReference commonPoolRegenerationRate;
        [SerializeField] private IntReference commonPoolExtractionRatePerParticipant;

        public void CreateCommonPool(int groupId) {
            var commonPoolObject = GameObject.Instantiate(commonPoolPrefab, Vector3.zero, Quaternion.identity);
            var commonPool = commonPoolObject.GetComponent<CommonPool>();
            commonPool.Initialize((ushort)groupId, commonPoolMaxResources.Value, commonPoolInitialResources.Value, commonPoolExtractionRatePerParticipant.Value, commonPoolRegenerationRate.Value);
            
            commonPools.Add(commonPool);
        }
    }
}