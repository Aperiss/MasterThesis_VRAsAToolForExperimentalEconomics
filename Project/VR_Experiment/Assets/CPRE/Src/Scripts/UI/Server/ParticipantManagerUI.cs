using System.Collections.Generic;
using CPRE.Scripts.Experiment;
using CPRE.Scripts.Experiment.DataContainers;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class ParticipantManagerUI : MonoBehaviour {

        [SerializeField] private ParticipantRuntimeSet participants;
        [SerializeField] private GameObject participantUIPrefab;

        private Dictionary<int, GameObject> _participantUIDictionary = new Dictionary<int, GameObject>();

        private void Update() {
            UpdateParticipantUIs();
        }

        private void UpdateParticipantUIs() {
            var currentParticipantIDs = new HashSet<int>();

            // Check and add new participant UIs
            foreach (var participant in participants.Items) {
                int participantID = participant.participantID.Value;
                currentParticipantIDs.Add(participantID);

                if (!_participantUIDictionary.ContainsKey(participantID)) {
                    AddParticipantUI(participantID);
                }
            }

            // Check and remove absent participant UIs
            foreach (var participantID in new List<int>(_participantUIDictionary.Keys)) {
                if (!currentParticipantIDs.Contains(participantID)) {
                    RemoveParticipantUI(participantID);
                }
            }
        }

        private void AddParticipantUI(int participantID) {
            var participantUI = Instantiate(participantUIPrefab, transform);
            participantUI.GetComponent<ParticipantUI>().SetParticipantID(participantID);
            _participantUIDictionary.Add(participantID, participantUI);
        }

        private void RemoveParticipantUI(int participantID) {
            if (_participantUIDictionary.TryGetValue(participantID, out GameObject participantUI)) {
                Destroy(participantUI);
                _participantUIDictionary.Remove(participantID);
            }
        }

        public void OnParticipantJoined(int participantID) {
            AddParticipantUI(participantID);
        }

        public void OnParticipantRemoved(int participantID) {
            RemoveParticipantUI(participantID);
        }
    }
}
