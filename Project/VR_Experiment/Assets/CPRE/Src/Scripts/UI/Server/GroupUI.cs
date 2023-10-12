using CPRE.Scripts.Experiment;
using CPRE.Scripts.Experiment.DataContainers;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class GroupUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI groupIDText;
        [SerializeField] private GroupRuntimeSet groups;
    
        private int _groupID;
    
        public int GetGroupID() {
            return _groupID;
        }
    
        public void SetGroupID(int groupID) {
            _groupID = groupID;
            groupIDText.text = "Group " + _groupID.ToString();
        }
        
        public void OnSelectTreatmentID(int treatmentId) {
            foreach (var group in groups.Items) {
                if(group.GroupId == _groupID) group.AssignTreatmentType((ushort)treatmentId);
            }
        }
        
    
    }
}
