using CPRE.Scripts.Experiment.Events.Votes;
using UnityEditor;
using UnityEngine;

namespace CPRE.Scripts.Experiment.Events.Editor {
    
    [CustomEditor(typeof(ExtractionVoteResponseEventChannel))]
    public class ExtractionVoteResponseEventChannelEditor : UnityEditor.Editor {
        private ushort _participantID;
        private bool _voteResults;
        private bool _participantVote;

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            ExtractionVoteResponseEventChannel eventChannel = (ExtractionVoteResponseEventChannel)target;
            _participantID = (ushort)EditorGUILayout.IntField("Participant GUID", _participantID);
            _voteResults = EditorGUILayout.Toggle("VoteResults", _voteResults);
            _participantVote = EditorGUILayout.Toggle("Participant Vote", _participantVote);
            
            if (GUILayout.Button("Raise Event")) {
                var responseData = new ExtractionVoteResponseData {
                    participantId = _participantID,
                    voteResults = _voteResults,
                    participantVote = _voteResults
                };
                eventChannel.Raise(responseData);
            }
        }
    }
}