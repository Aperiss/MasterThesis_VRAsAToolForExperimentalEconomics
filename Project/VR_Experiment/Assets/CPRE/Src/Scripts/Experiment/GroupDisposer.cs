using System.Linq;
using CPRE.Scripts.Experiment.DataContainers;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.Scripts.Experiment {
    public class GroupDisposer : MonoBehaviour {
        
        [SerializeField] private GroupRuntimeSet groups;
        [SerializeField] private CommonPoolRuntimeSet commonPools;
        [SerializeField] private SessionManagerRuntimeSet sessionManagers;

        [SerializeField] private IntEventChannel groupDeletedEventChannel;
        public void DeleteGroup() {
            if (groups.Items.Count == 0) return;
            var group = groups.Items.Last();
            
            var commonPool = group.GetCommonPool();
            if (commonPool != null) {
                commonPools.Remove(commonPool);
                commonPool.DeleteCommonPool();
            }

            var sessionManager = group.GetSessionManager();
            if (sessionManager != null) {
                sessionManagers.Remove(sessionManager);
                sessionManager.DeleteSessionManager();
            }
            
            groups.Remove(group);
            groupDeletedEventChannel.Raise(group.GroupId);
        }
    }
}