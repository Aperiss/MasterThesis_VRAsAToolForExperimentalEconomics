using CPRE.Scripts.Experiment.Events.Votes;
using UnityEditor;
using UnityEngine;

namespace CPRE.Scripts.Experiment.Events.Editor {
    [CustomEditor(typeof(ExtractionVoteRequestEventChannel))]
    public class ExtractionVoteRequestEventChannelEditor : UnityEditor.Editor {
        private ushort _participantID;
        private bool _vote;

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            ExtractionVoteRequestEventChannel eventChannel = (ExtractionVoteRequestEventChannel)target;
            _participantID = (ushort)EditorGUILayout.IntField("Participant GUID", _participantID);
            _vote = EditorGUILayout.Toggle("Vote", _vote);
            
            if (GUILayout.Button("Raise Event")) {
                var requestData = new ExtractionVoteRequestData {
                    participantId = _participantID,
                    vote = _vote
                };
                eventChannel.Raise(requestData);
            }
        }
    }
}