using CPRE.Scripts.Experiment.Events.Extractions;
using UnityEditor;
using UnityEngine;

namespace CPRE.Scripts.Experiment.Events.Editor {
    
    [CustomEditor(typeof(ExtractionResponseEventChannel))]
    public class ExtractionResponseEventChannelEditor : UnityEditor.Editor {
        private ushort _participantID;
        private bool _isApproved;
        private int _extractionAmount;

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            ExtractionResponseEventChannel eventChannel = (ExtractionResponseEventChannel)target;
            _participantID = (ushort)EditorGUILayout.IntField("Participant GUID", _participantID);
            _isApproved = EditorGUILayout.Toggle("Is Approved", _isApproved);
            _extractionAmount = EditorGUILayout.IntField("Extraction Amount", _extractionAmount);
            
            if (GUILayout.Button("Raise Event")) {
                var responseData = new ExtractionResponseData {
                    participantId = _participantID,
                    isApproved = _isApproved,
                    extractionAmount = _extractionAmount
                };
                eventChannel.Raise(responseData);
            }
        }
    }
}