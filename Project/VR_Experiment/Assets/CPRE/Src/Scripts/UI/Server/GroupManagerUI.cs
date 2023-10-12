using System.Collections.Generic;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class GroupManagerUI : MonoBehaviour {
    
        [SerializeField] private GameObject groupUIPrefab;
    
        private List<GameObject> groupUIs = new List<GameObject>();
        public void OnGroupCreated(int groupID) {
            var groupUI = Instantiate(groupUIPrefab, transform);
            groupUIs.Add(groupUI);
            groupUI.GetComponent<GroupUI>().SetGroupID(groupID);
        }

        public void OnGroupDeleted(int groupID) {
            foreach (var element in groupUIs) {
                if (element.GetComponent<GroupUI>().GetGroupID() != groupID) continue;
                groupUIs.Remove(element);
                Destroy(element);
                break;
            }
        }
    
    }
}
