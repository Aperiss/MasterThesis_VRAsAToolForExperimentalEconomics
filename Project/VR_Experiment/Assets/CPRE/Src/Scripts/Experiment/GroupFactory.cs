using System.Linq;
using CPRE.Scripts.Experiment.DataContainers;
using CPRE.Scripts.Experiment.Events;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.Scripts.Experiment {
    public class GroupFactory : MonoBehaviour {
        
        [SerializeField] private GroupRuntimeSet groups;
        
        [Header("Group Parameters")]
        [SerializeField] private IntReference maxNumberOfGroups;

        [Header("Event Triggers")]
        [SerializeField] private IntEventChannel groupCreatedEventChannel;
        
        public void CreateGroup () {
            if(groups.Items.Count >= maxNumberOfGroups) return;

            var newGroup = new Group((ushort)(groups.Items.Count + 1));
            
            groups.Add(newGroup);
            groupCreatedEventChannel.Raise(newGroup.GroupId);
        }
    }
}