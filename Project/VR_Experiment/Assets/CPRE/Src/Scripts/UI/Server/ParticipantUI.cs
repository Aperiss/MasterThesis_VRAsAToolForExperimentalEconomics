using CPRE.Scripts.Experiment;
using CPRE.Scripts.Experiment.DataContainers;
using TMPro;
using UnityEngine;
using Unity.Netcode;

namespace CPRE.Scripts.UI {
    public class ParticipantUI : MonoBehaviour {
        [SerializeField] private GroupRuntimeSet groups;
        [SerializeField] private ParticipantRuntimeSet participants;

        [SerializeField] private TextMeshProUGUI participantIDText;
        [SerializeField] private TMP_Dropdown groupDropdown;
        
        private int _participantID;
        private int _groupsCount;
        private int _selectedGroupID;

        private void Start() {
            _groupsCount = 0;
            _selectedGroupID = 0;
        }

        private void Update() {
            if(_groupsCount != groups.Items.Count) {
                _groupsCount = groups.Items.Count;
                groupDropdown.ClearOptions();
                groupDropdown.options.Add(new TMP_Dropdown.OptionData("None"));
                foreach (var group in groups.Items) {
                    groupDropdown.options.Add(new TMP_Dropdown.OptionData(group.GroupId.ToString()));
                }

                if (_selectedGroupID > _groupsCount) {
                    _selectedGroupID = 0;
                }
                groupDropdown.value = _selectedGroupID;
                groupDropdown.captionText.text = groupDropdown.options[_selectedGroupID].text;
            }
        }

        public int GetParticipantID() {
            return _participantID;
        }
        public void SetParticipantID(int participantID) {
            _participantID = participantID;
            participantIDText.text = "Participant " + _participantID.ToString();
        }
        
        public void OnSelectGroupID(int groupID) {
            Debug.Log("Setting groupID to " + groupID + " for participant " + _participantID);
            _selectedGroupID = groupID;
            foreach (var participant in participants.Items) {
                if(participant.participantID.Value == (ushort)_participantID) {
                    participant.AssignGroupID((ushort)groupID);
                    
                }
            }
        }

    }
}
